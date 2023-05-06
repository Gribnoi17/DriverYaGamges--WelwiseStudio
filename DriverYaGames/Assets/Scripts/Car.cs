using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _explosionParticleSystem;

    private bool _isZone;

    private ZoneObjectsDetector _detector;

	public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    private void Start()
    {
        _detector = FindObjectOfType<ZoneObjectsDetector>();
    }

    private void Update()
    {
        Drive();
        CheckDeleting();
    }

    private void Drive()
    {
        transform.position += _speed * Time.deltaTime * Vector3.back;
    }

    private void CheckDeleting()
    {
        if (transform.position.z <= -85f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag != "Shield" && gameObject.tag != "SpeedBooster" && gameObject.tag != "ItemCamera")
        {
            GameObject explosion = Instantiate(_explosionParticleSystem, transform.position, Quaternion.identity);
            Destroy(explosion, 1.0f);
            if (_isZone == false)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
                _detector.CountOfCars -=1;
                _isZone = false;
            }
                
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if ( gameObject.tag != "Shield" && gameObject.tag != "SpeedBooster" && gameObject.tag != "ItemCamera")
            _isZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (gameObject.tag != "Shield" && gameObject.tag != "SpeedBooster")
            _isZone = false;
    }


}
