using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarWhoosed : MonoBehaviour
{
    [SerializeField] private float _dist;
    [SerializeField] private AudioClip _clip;

    [SerializeField]private AudioSource _source;

    Vector3 _carPos = new Vector3(0, 0, 0);

    private void Start()
    {
        _source= GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (_source.clip == null && Vector3.Distance(transform.position, _carPos) <= _dist)
        {
            _source.clip = _clip;
            _source.Play();
        }
    }
}
