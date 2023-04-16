using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private GameObject _explosionParticleSystem;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PoliceCar>())
        {
            Vector3 oppositeTheCamera = new Vector3(0f, 3f, -26.83f);
            GameObject explosion = Instantiate(_explosionParticleSystem, oppositeTheCamera, Quaternion.Euler(180f, 0f, 0f)); //ѕоворот, чтобы частицы летели на камеру
            Destroy(explosion, 3.5f);
        }
    }
}
