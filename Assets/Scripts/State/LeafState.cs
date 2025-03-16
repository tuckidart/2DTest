public enum ELeafState
{
    NONE = 0,

    IDLE,
    HOVER,
    DRAG,
    MOVEMENT,
    SWIRL
}

public abstract class LeafState
{
    protected Leaf _leaf = null;

    public abstract void EnterState(Leaf leaf);
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
}