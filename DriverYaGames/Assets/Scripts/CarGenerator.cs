using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Moroutines;
using Redcode.Extensions;
//using Unity.Android.Types;
using Redcode.Tweens;
using System.Threading;

public class CarGenerator : MonoBehaviour
{
    [SerializeField] private Transform _linePoint;
    [SerializeField] private CameraController _camera;
    [SerializeField] private Car[] _carsPrefabs;
    [SerializeField] private float _period;
    public float SpawnPeriod { get { return _period; } set { _period = value; } }


    private float _additionalSpeed;
    private int _countRoutinesSpeedTick = 5;
    private int _countRoutinesPeriodTick = 6;
    private void Start()
    {
        int _countRoutinesTick = 0;
        Moroutine.Run(Routines.Repeat(_countRoutinesSpeedTick, Routines.Delay(6f, () =>
        {
            _additionalSpeed += 10f;
        })));

        Moroutine.Run(Routines.Repeat(_countRoutinesPeriodTick, Routines.Delay(3f, () =>
        {
            _countRoutinesTick++;
            //_period -= 0.22f;
            //Переделать кусок кода с камерой
            if (_countRoutinesTick == 5)
                _camera.transform.DoEulerAnglesY(3.5f, _camera.Period, Ease.InOutSine, int.MaxValue, LoopType.Mirror).Play();
            if (_countRoutinesTick == 6)
                _camera.enabled = true;


        })));

        Moroutine.Run( Routines.Repeat(-1, GeneratorCarEnumerable()));
    }

    private IEnumerable GeneratorCarEnumerable()
    {
        yield return new WaitForSeconds(_period);

        
        var car = Instantiate(_carsPrefabs[Random.Range(0, _carsPrefabs.Length)],
            _linePoint.GetChild(Random.Range(0, _linePoint.childCount)).position.WithZ(transform.position.z),
            Quaternion.Euler(0f, 180f, 0f), transform);

        car.Speed += _additionalSpeed;

    }
}
