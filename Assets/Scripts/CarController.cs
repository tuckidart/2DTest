using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Leaf _leaf = null;
    [SerializeField]
    private float _timeToRelocate = 30f;

    private float _carSpeed = 25f;
    private float _timer = 0f;

    private void Start()
    {
        _timer = _timeToRelocate;
    }

    // Update is called once per frame
    private void Update()
    {
        _timer -= Time.deltaTime;

        float distanceX = Mathf.Abs(transform.position.x - _leaf.transform.position.x);

        if (_timer <= 0 && distanceX > 30)
        {
            RelocateCar();
            return;
        }

        transform.position += Vector3.right * Time.deltaTime * _carSpeed;

        if (Vector2.Distance(transform.position, _leaf.Position) < 10 && transform.position.x > _leaf.Position.x)
        {
            _leaf.ChangeState(ELeafState.SWIRL);
        }
    }

    private void RelocateCar()
    {
        transform.position = new Vector3(_leaf.Position.x - 30f, transform.position.y, transform.position.z);
        _timer = _timeToRelocate;
    }
}
