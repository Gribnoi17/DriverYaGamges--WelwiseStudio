using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnImpact : MonoBehaviour
{
    [SerializeField] private GameObject _explosionParticleSystem;

    private void Start()
    {
        EventManager.PlayerDied += PlayExplosionParticle;
    }

    private void OnDestroy()
    {
        EventManager.PlayerDied -= PlayExplosionParticle;
    }

    private void PlayExplosionParticle()
    {
        GameObject explosion = Instantiate(_explosionParticleSystem, transform.position, Quaternion.identity);
        Destroy(explosion, 1.0f);
        Destroy(gameObject);
    }

}
