using UnityEngine;

public class Swirl : LeafState
{
    private CustomRigidbody _customRB = null;

    // Controls the direction/speed the spiral translates
    private Vector2 _linearVelocity = Vector2.zero;

    private float _timeScale = 5f;
    private float _timer = 0;
    private float _completedRotations = 0;

    public override void EnterState(Leaf leaf)
    {
        _leaf = leaf;
        _customRB = _leaf.CustomRigidbody;
        _customRB.UseGravity = true;

        _timer = 0f;
        _linearVelocity = new Vector2(Random.Range(5f, 10f), Random.Range(2f, 5f));
        _completedRotations = Random.Range(2, 4);
    }

    public override void UpdateState() { }

    public override void FixedUpdateState()
    {
        // Adjust time.
        _timer += _timeScale * Time.fixedDeltaTime;

        // Calculate the angle at this time.
        float angle = _leaf.AngularVelocity * _timer;

        if (angle >= _completedRotations * Mathf.PI)
        {
            _leaf.ChangeState(ELeafState.MOVEMENT);
            return;
        }

        // Calculate the circular motion.
        Vector2 swirlMotion = new Vector2(Mathf.Cos(angle) * _leaf.Radius.x, Mathf.Sin(angle) * _leaf.Radius.y);

        // Apply the velocity to the custom Rigidbody.
        _customRB.SetVelocity(_linearVelocity + swirlMotion);

        // Calculate the angle in radians and convert to degrees
        angle = Mathf.Atan2(_customRB.GetVelocity().y, _customRB.GetVelocity().x) * Mathf.Rad2Deg + 90;

        // Apply rotation to the leaf
        _leaf.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void ExitState() { }
}
