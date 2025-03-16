using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float ParallaxEffect = 0;

    [SerializeField]
    private Camera _camera = null;
    [SerializeField]
    private SpriteRenderer[] _spriteRenderers = null;

    private float _startX = 0;
    private float _startY = 0;
    private float _length = 0;

    private void Start()
    {
        _startX = transform.position.x;
        _startY = transform.position.y;

        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _length += _spriteRenderers[i].bounds.size.x;
        }
    }

    private void Update()
    {
        Vector2 distance = _camera.transform.position * ParallaxEffect;
        float movementX = _camera.transform.position.x * (1 - ParallaxEffect);

        transform.position = new Vector3(_startX + distance.x, _startY + distance.y, transform.position.z);

        if (movementX > _startX + _length)
        {
            _startX += _length;
        }
        else if (movementX < _startX - _length)
        {
            _startX -= _length;
        }
    }
}
