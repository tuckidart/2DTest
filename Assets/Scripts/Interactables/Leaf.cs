using UnityEngine;

public class Leaf : Interactable
{
    [SerializeField]
    private LeafData _data = null;

    [SerializeField]
    private CustomRigidbody _customRigidbody = null;
    public CustomRigidbody CustomRigidbody => _customRigidbody;

    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    [Space]

    [SerializeField]
    private AudioSource _audioSource = null;

    public Vector3 Position => transform.position;

    [Space]
    [Space]

    //The variables the model gives us to control
    public float RandomAngularVelocity = 0f;
    public float SwayFrequency = 0f;
    public float SwayAmplitude = 0f;
    public float ThrowDamping = 0f;
    public float AngularVelocity = 0f;
    public Vector2 Radius = Vector2.zero;

    private LeafState _leafState = null;
    private Idle _idle = null;
    private Hover _hover = null;
    private Drag _drag = null;
    private Movement _movement = null;
    private Swirl _swirl = null;

    private void Start()
    {
        //The LeafData is the model and this(Leaf) is the controller
        //Sprite should be in a LeafView to complete the MVC, but since it's only a single sprite for now, let's keep it here.

        _spriteRenderer.sprite = _data.Sprite;
        RandomAngularVelocity = _data.RandomAngularVelocity;
        SwayFrequency = _data.SwayFrequency;
        SwayAmplitude = _data.SwayAmplitude;
        ThrowDamping = _data.ThrowDamping;
        AngularVelocity = _data.AngularVelocity;
        Radius = _data.Radius;


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
