using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    public float Speed = 5f;
    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private float _turnOffZCoordinate;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private bool _spawnByPoints = false;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    private void Update()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.Translate(_moveDirection * Time.deltaTime * Speed, Space.World);
            if (child.position.z <= _turnOffZCoordinate)
            {
                if(_spawnByPoints ==true)
                    ResetPositionByPoints(child);
                else
                    ResetPosition(child);
            }
        }        
    }


    private void ResetPosition(Transform pos)
    {
        pos.position = startPosition;
    }

    private void ResetPositionByPoints(Transform pos)
    {
        if(spawnPoints != null)
        {
            pos.position = spawnPoints[Random.Range(0, spawnPoints.Count)].position;
            StartCoroutine(ScaleUpOverTime(pos));
        }
        else
        {
            throw new System.Exception($"У вас стоит галочка спавна по точкам, но точки не назначены в объекте {gameObject.name}") ;
        }
    }


    private IEnumerator ScaleUpOverTime(Transform trans)
    {
        float elapsedTime = 0f;
        Vector3 startingScale = Vector3.zero;
        Vector3 targetScale = trans.localScale;

        while (elapsedTime < 2f) //Время увеличения
        {
            trans.localScale = Vector3.Lerp(startingScale, targetScale, (elapsedTime / 2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trans.localScale = targetScale;
    }


}
