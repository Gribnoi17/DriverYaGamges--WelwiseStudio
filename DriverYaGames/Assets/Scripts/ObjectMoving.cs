using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    [Header("Movement Settings")]
    public float StartSpeed = 5f;
    [SerializeField, Tooltip("The direction of movement")]
    private Vector3 _moveDirection;
    [SerializeField, Tooltip("The Z-coordinate at which the object will be reset to its start position")]
    private float turnOffZCoordinate;

    [Header("Spawn Settings")]
    [SerializeField, Tooltip("The starting position of the object")]
    private Vector3 _startPosition;

    [SerializeField] private Vector3 addToSpawnPosition;

    [SerializeField, Tooltip("Spawn the object at random points from the list")]
    private bool _spawnByPoints = false;

    [SerializeField, Tooltip("A list of points where the object can spawn if Spawn By Points is enabled")]

    private List<Transform> _spawnPoints = new List<Transform>();

    [HideInInspector] public float CurrentSpeed = 5f;

    private void Start()
    {
        CurrentSpeed = StartSpeed;
    }
    
    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.Translate(_moveDirection * Time.deltaTime * CurrentSpeed, Space.World);
            if (child.position.z <= turnOffZCoordinate)
            {
                if (_spawnByPoints)
                    ResetPositionByPoints(child);
                else
                    ResetPosition(child);
            }
        }
    }

    private void ResetPosition(Transform pos)
    {
       // pos.position = new Vector3(_startPosition.x, _startPosition.y, lastChild + Mathf.Abs(_moveDirection.z * pos.localScale.z));
    }

    private void ResetPositionByPoints(Transform pos)
    {
        if (_spawnPoints != null && _spawnPoints.Count > 0)
        {
            pos.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
            StartCoroutine(ScaleUpOverTime(pos));
        }
        else
        {
            throw new System.Exception($"The spawn by points option is enabled but there are no spawn points assigned to the {gameObject.name} object.");
        }
    }

    private IEnumerator ScaleUpOverTime(Transform trans)
    {
        float elapsedTime = 0f;
        Vector3 startingScale = Vector3.zero;
        Vector3 targetScale = trans.localScale;

        while (elapsedTime < 2f) // Scaling time
        {
            trans.localScale = Vector3.Lerp(startingScale, targetScale, (elapsedTime / 2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trans.localScale = targetScale;
    }
}
