using UnityEngine;

public class Movement : LeafState
{
    private Vector3 _lastMousePosition = Vector3.zero;
    private Vector3 _windForce = Vector3.zero;
    private float _targetRotationZ = 0f; // Target rotation angle for smooth transition
    private float _resistance = 0.999f;

    private CustomRigidbody _customRB = null;

    public override void EnterState(Leaf leaf)
    {
        _leaf = leaf;
        _customRB = _leaf.CustomRigidbody;
        _lastMousePosition = Input.mousePosition; // Initialize last mouse position
    }

    public override void UpdateState()
    {
        // Calculate wind force based on mouse movement
        Vector3 mouseDelta = (Input.mousePosition - _lastMousePosition) * 15.0f; // Scale factor to adjust wind effect
        _windForce = new Vector3(mouseDelta.x, mouseDelta.y, 0);
        _lastMousePosition = Input.mousePosition;

        // Calculate target rotation based on wind direction
        if (_windForce.magnitude > 0.1f) // Prevent jittering when no wind is applied
        {
            _targetRotationZ = Mathf.Atan2(_windForce.y, _windForce.x) * Mathf.Rad2Deg;
        }
    }

    public override void FixedUpdateState()
    {
        if (_leaf.Position.y < Constants.GroundLevel)
        {
            _leaf.ChangeState(ELeafState.IDLE);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            // Apply wind force
            _customRB.AddForce(_windForce, ForceMode2D.Force);
        }

        // Apply a periodic sway motion to simulate a leaf fluttering
        float sway = Mathf.Sin(Time.time * _leaf.SwayFrequency) * _leaf.SwayAmplitude;
        _customRB.AddForce(new Vector3(sway, 0, 0), ForceMode2D.Force);

        // Smoothly rotate the leaf towards wind direction
        float smoothedRotationZ = Mathf.LerpAngle(_leaf.transform.eulerAngles.z, _targetRotationZ, Time.deltaTime * 2f);
        _leaf.transform.rotation = Quaternion.Euler(0, 0, smoothedRotationZ);

        // Add small random velocity to make the leaf rotate slightly
        float randomAngularVel = (Random.value - 0.5f) * _leaf.RandomAngularVelocity;
        _customRB.SetAngularVelocity(new Vector3(0, 0, randomAngularVel));

        // Apply drag to the linear velocity (slow down if moving too fast)
        if (_customRB.GetVelocity().magnitude > 0.1f) // Apply drag only if the leaf is moving
        {
            _customRB.SetVelocity(_customRB.GetVelocity() * _resistance); // Slow down the leaf by resistance factor
        }

        // Limit the leaf's height
        if (_leaf.Position.y > Constants.MaxHeight)
        {
            _leaf.transform.position = new Vector3(_leaf.Position.x, Constants.MaxHeight, _leaf.Position.z);
            _customRB.SetVelocity(new Vector3(_customRB.GetVelocity().x, 0, 0)); // Stop upward movement
        }
    }

    public override void ExitState()
    {
        _customRB.SetVelocity(Vector3.zero);
        _customRB.SetAngularVelocity(Vector3.zero);
        _customRB.UseGravity = false;
    }
}
