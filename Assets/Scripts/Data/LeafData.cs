using UnityEngine;

[CreateAssetMenu(fileName = "Leaf Data", menuName = "Game/New Leaf Data")]
public class LeafData : ScriptableObject
{
    [SerializeField]
    private Sprite _sprite = null;

    [Space]

    [SerializeField]
    private float _randomAngularVelocity = 500f;
    [SerializeField]
    private float _swayFrequency = 2f;
    [SerializeField]
    private float _swayAmplitude = 10f;
    [SerializeField]
    private float _throwDamping = 0.07f; // Reduce how far the leaf is thrown
    [SerializeField]
    private float _angularVelocity = 0.5f; // Controls the rotational speed of the spiral.
    [SerializeField]
    private Vector2 _radius = new Vector2(20f, 20f); // Controls the radius of the circular motion for X/Y axis.

    public Sprite Sprite => _sprite;
    public float RandomAngularVelocity => _randomAngularVelocity;
    public float SwayFrequency => _swayFrequency;
    public float SwayAmplitude => _swayAmplitude;
    public float ThrowDamping => _throwDamping;
    public float AngularVelocity => _angularVelocity;
    public Vector2 Radius => _radius;
}
