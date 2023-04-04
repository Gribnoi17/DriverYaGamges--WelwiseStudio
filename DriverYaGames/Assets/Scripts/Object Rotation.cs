using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    public float speed = 5f; // �������� �������� �������
    public Vector3 axis = Vector3.up; // ��� ��������

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        // ������� ������ � ������� DoTween
        transform.DORotate(axis * 360f, speed, RotateMode.LocalAxisAdd)
                 .SetEase(Ease.Linear)
                 .OnComplete(() => Rotate()); // ��������� �������� ����� ����������
    }
}
