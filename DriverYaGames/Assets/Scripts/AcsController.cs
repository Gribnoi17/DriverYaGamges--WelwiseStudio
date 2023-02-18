using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcsController : MonoBehaviour
{
    [SerializeField] private Rigidbody rg;
    [SerializeField] private float speed;
    [SerializeField] private float deadZone;

    [Header("Coordinates")]
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        if (acceleration.x > deadZone && rg.transform.position.x < maxX)
        {
            rg.velocity = new Vector3(acceleration.x * speed, 0f, 0f);
        }
        else if (acceleration.x < -deadZone && rg.transform.position.x > minX)
        {
            rg.velocity = new Vector3(acceleration.x * speed, 0f, 0f);
        }
        else
        {
            rg.velocity = new Vector3(0f, 0f, 0f);
        }
    }
}
