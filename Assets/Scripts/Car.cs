using System.Collections;
using UnityEngine;


public class Car : MonoBehaviour
{
    private const string _deathParticleName = "DeathParticle";

    [SerializeField] private float _speed;

    //private float _heightParticleRelativeCar = 0.4f;
    //private Object _deathParticleObject;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }


    //private void Start()
    //{
    //    _deathParticleObject = Resources.Load(_deathParticleName);
    //}

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
        //GameObject deathParticle = (GameObject)Instantiate(_deathParticleObject);
        //deathParticle.transform.position = new Vector3(transform.position.x, transform.position.y + _heightParticleRelativeCar, transform.position.z);
        Destroy(gameObject);

    }


}
