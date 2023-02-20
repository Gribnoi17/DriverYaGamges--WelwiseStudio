using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;


    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        _particleSystem.Play();
    }
}
