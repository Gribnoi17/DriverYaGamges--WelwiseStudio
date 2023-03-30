using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Moroutines;
using Redcode.Extensions;
using Redcode.Tweens;

public class CarGenTest : MonoBehaviour
{
    [SerializeField] private Transform _linePoint;
    [SerializeField] private CameraController _camera;
    [SerializeField] private Car[] _carsPrefabs;
    [SerializeField] private float _period;
    [SerializeField] private Car[] _auxiliary;
    [SerializeField] private int _poolSize = 10;

    private Queue<Car> _carPool = new Queue<Car>();

    private float _additionalSpeed;
    private int _countRoutinesSpeedTick = 5;
    private int _countRoutinesPeriodTick = 6;

    private void Start()
    {
        InitializeCarPool();

        int _countRoutinesTick = 0;
        Moroutine.Run(Routines.Repeat(_countRoutinesSpeedTick, Routines.Delay(6f, () =>
        {
            _additionalSpeed += 10f;
        })));

        Moroutine.Run(Routines.Repeat(_countRoutinesPeriodTick, Routines.Delay(3f, () =>
        {
            _countRoutinesTick++;

            // if (_countRoutinesTick == 5)
            //     _camera.transform.DoEulerAnglesY(3.5f, _camera.Period, Ease.InOutSine, int.MaxValue, LoopType.Mirror).Play();
            // if (_countRoutinesTick == 6)
            //     _camera.enabled = true;
        })));

        Moroutine.Run(Routines.Repeat(-1, GeneratorCarEnumerable()));
    }

    private void InitializeCarPool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            Car car = Instantiate(_carsPrefabs[Random.Range(0, _carsPrefabs.Length)],
                _linePoint.position.WithZ(transform.position.z),
                Quaternion.Euler(0f, 180f, 0f), transform);

            car.gameObject.SetActive(false);
            _carPool.Enqueue(car);
        }
    }

    private IEnumerable GeneratorCarEnumerable()
    {
        while (true)
        {
            yield return new WaitForSeconds(_period);
            Car car = null;

            if (Random.Range(0, 8) == 0)
            {
                car = GetCarFromPool(_auxiliary[Random.Range(0, _auxiliary.Length)]);
            }
            else
            {
                if (_carsPrefabs[0] != null)
                {
                    car = GetCarFromPool(_carsPrefabs[Random.Range(0, _carsPrefabs.Length)]);
                }
            }

            car.transform.position = _linePoint.GetChild(Random.Range(0, _linePoint.childCount)).position.WithZ(transform.position.z);
            car.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            car.gameObject.SetActive(true);
            car.Speed += _additionalSpeed;
        }
    }

    private Car GetCarFromPool(Car prefab)
    {
        Car car = _carPool.Count > 0 ? _carPool.Dequeue() : Instantiate(prefab, transform);
        car.transform.SetParent(transform);
        return car;
    }

    public void ReturnCarToPool(Car car)
    {
        car.gameObject.SetActive(false);
        _carPool.Enqueue(car);
    }

}
