using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    // Update is called once per frame
    void Update()
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
}
