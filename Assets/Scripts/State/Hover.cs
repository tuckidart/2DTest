using UnityEngine;

public class Hover : LeafState
{
    private Vector3 _hoverScale = new Vector3(1.5f, 1.5f, 1.5f);

    private float _elapsedLerp = 0.0f;
    private readonly float _animationTime = 0.5f;

    private bool _animating = false;

    public override void EnterState(Leaf leaf)
    {
        _leaf = leaf;
        _animating = true;
    }

    public override void UpdateState()
    {
        if (!_leaf.IsHovering)
        {
            _leaf.ChangeState(ELeafState.IDLE);
            return;
        }

        if (!_animating)
        {
            return;
        }

        _elapsedLerp += Time.deltaTime / _animationTime;
        _leaf.transform.localScale = Vector3.Lerp(Vector3.one, _hoverScale, _elapsedLerp);

        if (_elapsedLerp >= 0.5f)
        {
            _animating = false;
        }
    }

    public override void FixedUpdateState() { }
    public override void ExitState()
    {
        _leaf.transform.localScale = Vector3.one;
        _elapsedLerp = 0.0f;
        _animating = false;
    }
}