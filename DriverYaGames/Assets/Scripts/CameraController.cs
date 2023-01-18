using Redcode.Tweens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _period = 6f;
    public float Period
    {
        get => _period;
        set => _period = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.DoEulerAnglesY(-3.5f, _period, Ease.InOutSine, int.MaxValue, LoopType.Mirror).Play();
    }


}
