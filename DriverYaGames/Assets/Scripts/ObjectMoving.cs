using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    public float Speed = 5f;
    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private float _turnOffZCoordinate;
    [SerializeField] private Vector3 startPosition;

    private void Update()
    {
        transform.Translate(_moveDirection * Time.deltaTime * Speed, Space.Self);

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.position.z <= _turnOffZCoordinate)
            {
                ResetPosition(child);
            }
        }        
    }


    private void ResetPosition(Transform pos)
    {
        pos.position = startPosition;
    }

    
}
