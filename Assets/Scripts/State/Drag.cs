using UnityEngine;

public class Drag : LeafState
{
    private Vector3 _lastMousePosition = Vector3.zero;
    private Vector3 _swipeVelocity = Vector3.zero;

    private CustomRigidbody _customRB = null;

    public override void EnterState(Leaf leaf)
    {
        _leaf = leaf;
        _customRB = _leaf.CustomRigidbody;

        _customRB.UseGravity = false;
        _customRB.SetVelocity(Vector3.zero);
        _customRB.SetAngularVelocity(Vector3.zero);
        _lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public override void UpdateState()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Keep in 2D plane
        mousePosition.z = 0;

        // Calculate swipe velocity
        _swipeVelocity = (mousePosition - _lastMousePosition) / Time.deltaTime;
        _lastMousePosition = mousePosition;

        //go to position
        _leaf.transform.position = Vector2.Lerp(_leaf.Position, mousePosition, Time.deltaTime * 10);

        // Calculate direction from leaf to mouse
        Vector3 direction = mousePosition - _leaf.Position;

        // Calculate the angle in radians and convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        // Apply rotation to the leaf
        _leaf.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public override void FixedUpdateState() { }
    public override void ExitState()
    {
        _customRB.UseGravity = true;
        _customRB.SetVelocity(_swipeVelocity * _leaf.ThrowDamping);
    }
}