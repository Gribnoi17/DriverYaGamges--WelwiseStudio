using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Tweens;
using Redcode.Moroutines;

public class CarLightAnimation : MonoBehaviour
{
    [SerializeField] private Light _light;
    [Tooltip("�� ������ ����� intensity ����� ���� ��������")] [SerializeField] private float _intensity;

    private void Start()
    {
         _light.DoIntensity(_intensity, 1f, Ease.InOutCubic, int.MaxValue, LoopType.Mirror).Play();
    }

}
