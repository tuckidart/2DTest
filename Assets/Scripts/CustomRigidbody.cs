using UnityEngine;

public class CustomRigidbody : MonoBehaviour
{
    [Header("Physics Settings")]

    private Vector3 _forces = Vector3.zero;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _angularVelocity = Vector3.zero;

    [SerializeField]
    private float _mass = 1f;
    [SerializeField]
    private float _gravity = -9.81f;
    public bool UseGravity = true;

    private void FixedUpdate()
    {
        if (UseGravity)
        {
            // Apply gravity directly without accumulating forces
            _velocity += Vector3.up * _gravity * Time.fixedDeltaTime;
        }

        // Apply forces to velocity
        _velocity += (_forces / _mass) * Time.fixedDeltaTime;

        // Update position
        transform.position += _velocity * Time.fixedDeltaTime;

        // Apply angular velocity
        Quaternion deltaRotation = Quaternion.Euler(_angularVelocity * Time.fixedDeltaTime);
        transform.rotation *= deltaRotation;

        // Reset forces for the next frame
        _forces = Vector3.zero;
    }

    public void AddForce(Vector3 force, ForceMode2D mode = ForceMode2D.Force)
    {
        switch (mode)
        {
            case ForceMode2D.Force:
                _forces += force; // Accumulate force instead of modifying velocity directly
                break;

            case ForceMode2D.Impulse:
                _velocity += force / _mass; // Instant velocity change
                break;
        }
    }

    public Vector3 GetVelocity() => _velocity;
    public Vector3 GetAngularVelocity() => _angularVelocity;
    public void SetVelocity(Vector3 newVelocity) { _velocity = newVelocity; }
    public void SetAngularVelocity(Vector3 newAngularVelocity) { _angularVelocity = newAngularVelocity; }
}
