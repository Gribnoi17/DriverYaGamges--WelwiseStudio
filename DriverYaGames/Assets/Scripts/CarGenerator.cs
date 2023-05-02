using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Moroutines;
using Redcode.Extensions;
using Redcode.Tweens;
using System.Threading;

public class Generator : MonoBehaviour
{
    [SerializeField] private Transform _linePoint;
    [SerializeField] private Car[] _carsPrefabs;
    [SerializeField] private float _period;
    [SerializeField] private Car[] _auxiliary;
    //_auxiliary[0] - нитро _auxiliary[1] - щит

    public float SpawnPeriod { get { return _period; } set { _period = value; } }

    private float _additionalSpeed;
    private int _countRoutinesSpeedTick = 5;
    private bool _canSpawnAuxiliary = true;
    [HideInInspector] public bool needSpawnShield = true;

    private void Start()
    {

        Moroutine.Run(Routines.Repeat(_countRoutinesSpeedTick, Routines.Delay(6f, () =>
        {
            _additionalSpeed += 10f;
        })));

        Moroutine.Run( Routines.Repeat(-1, GeneratorCarEnumerable()));
    }

    private IEnumerable GeneratorCarEnumerable()
    {

        if (_linePoint == null)
        {
            yield break; // выходим из метода, если ссылка на объект _linePoint не установлена
        }

        yield return new WaitForSeconds(_period);
        Car car = null;
        needSpawnShield = true;
        if (Random.Range(0, 11) == 0 & _canSpawnAuxiliary)
        {
            StartCoroutine(StartAuxiliaryBreak());
            if(needSpawnShield == true)
            {
                car = Instantiate(_auxiliary[Random.Range(0, _auxiliary.Length)],
                _linePoint.GetChild(Random.Range(0, _linePoint.childCount)).position.WithZ(transform.position.z),
                Quaternion.Euler(0f, 0f, 0f), transform);
            }
            else
            {
                car = Instantiate(_auxiliary[0],
                _linePoint.GetChild(Random.Range(0, _linePoint.childCount)).position.WithZ(transform.position.z),
                Quaternion.Euler(0f, 0f, 0f), transform);
            } 
        }
        else
        {
            if (_carsPrefabs != null && _carsPrefabs.Length != 0)
            {
                car = Instantiate(_carsPrefabs[Random.Range(0, _carsPrefabs.Length)],
                _linePoint.GetChild(Random.Range(0, _linePoint.childCount)).position.WithZ(transform.position.z),
                Quaternion.Euler(0f, 180f, 0f), transform);
            }     
           
        }

        car.Speed += _additionalSpeed;
    }


    private IEnumerator StartAuxiliaryBreak()
    {
        _canSpawnAuxiliary = false;
        yield return new WaitForSeconds(2f);
        _canSpawnAuxiliary = true;
    }

    public void RemoveAllChildren()
    {
        // Перебираем всех детей объекта и удаляем их
        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }
}
