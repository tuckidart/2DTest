using UnityEngine;

public class Leaf : Interactable
{
    [SerializeField]
    private AudioSource _audioSource = null;

    [SerializeField]
    private CustomRigidbody _customRigidbody = null;
    public CustomRigidbody CustomRigidbody => _customRigidbody;
    public Vector3 Position => transform.position;

    public float RandomAngularVelocity = 500f;
    public float SwayFrequency = 2f;
    public float SwayAmplitude = 10f;

    private LeafState _leafState = null;
    private Idle _idle = null;
    private Hover _hover = null;
    private Drag _drag = null;
    private Movement _movement = null;
    private Swirl _swirl = null;

    private void Start()
    {
        _idle = new Idle();
        _hover = new Hover();
        _drag = new Drag();
        _movement = new Movement();
        _swirl = new Swirl();

        ChangeState(ELeafState.MOVEMENT);
    }

    public void Update()
    {
        _leafState?.UpdateState();
    }

    private void FixedUpdate()
    {
        _leafState?.FixedUpdateState();
    }

    public void ChangeState(ELeafState leafState)
    {
        _leafState?.ExitState();

        switch (leafState)
        {
            case ELeafState.IDLE:
                _leafState = _idle;
                break;
            case ELeafState.HOVER:
                _leafState = _hover;
                break;
            case ELeafState.DRAG:
                _leafState = _drag;
                break;
            case ELeafState.MOVEMENT:
                _leafState = _movement;
                break;
            case ELeafState.SWIRL:
                _leafState = _swirl;
                break;
            default:
                break;
        }

        _leafState?.EnterState(this);
    }

    public override void OnMouseDown()
    {
        if (_leafState == _hover)
        {
            ChangeState(ELeafState.DRAG);
        }
    }

    public override void OnMouseUp()
    {
        if (_leafState == _drag)
        {
            ChangeState(ELeafState.MOVEMENT);
        }
    }

    public void AdjustWindVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
