using UnityEngine;

public class Idle : LeafState
{
    public override void EnterState(Leaf leaf)
    {
        _leaf = leaf;
    }

    public override void UpdateState()
    {
        if (_leaf.IsHovering)
        {
            _leaf.ChangeState(ELeafState.HOVER);
            return;
        }
    }

    public override void FixedUpdateState() { }
    public override void ExitState() { }
}