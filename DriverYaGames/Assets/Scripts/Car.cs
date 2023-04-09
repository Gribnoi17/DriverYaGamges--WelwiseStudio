using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _explosionParticleSystem;

	public float Speed
    {
        get => _speed;
        set => _speed = value;
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
          if (gameObject.tag != "Shield" && gameObject.tag != "SpeedBooster")
          {
               GameObject explosion = Instantiate(_explosionParticleSystem, transform.position, Quaternion.identity);
               Destroy(explosion, 1.0f);
               Destroy(gameObject);
          }
    }


}
