using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    public float speed = 5f; // скорость вращения объекта
    public Vector3 axis = Vector3.up; // ось вращения
    public bool isStupidCar = false;
    
    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        // вращаем объект с помощью DoTween
        transform.DORotate(axis * 360f, speed, RotateMode.WorldAxisAdd)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1)
                 .SetLink(gameObject);
    }

}
