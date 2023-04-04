using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    public float speed = 5f; // скорость вращения объекта
    public Vector3 axis = Vector3.up; // ось вращения

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        // вращаем объект с помощью DoTween
        transform.DORotate(axis * 360f, speed, RotateMode.LocalAxisAdd)
                 .SetEase(Ease.Linear)
                 .OnComplete(() => Rotate()); // повторяем вращение после завершения
    }
}
