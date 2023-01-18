using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Tweens;

public class PoliceCar : MonoBehaviour
{
    [SerializeField] private Transform _linePoint;
    [SerializeField] private float _animationDuration;
    private int _lineIndex = 1;
    private Playable _animation;

    private void Start()
    {
        SwipeDetection.SwipeEvent += OnSwipe;
    }

    private void OnSwipe(Vector2 direction)
    {
        MoveSwipe(direction);      
    }

    // Update is called once per frame
    void Update()
    {
        MoveKeyboard(KeyCode.A, -1);
        MoveKeyboard(KeyCode.D, 1);
    }


    
    private void MoveKeyboard(KeyCode key, int direction)
    {
        if ((_animation == null || _animation.PlayedTime == _animationDuration) && Input.GetKeyDown(key))
        {
            _lineIndex = Mathf.Clamp(_lineIndex + direction, 0, _linePoint.childCount - 1);
            var point = _linePoint.GetChild(_lineIndex);
            _animation = transform.DoPositionX(point.transform.position.x, _animationDuration, Ease.InOutCirc).Play();
        }
    }

    private void MoveSwipe(Vector2 direction)
    {
        int _tempDirection;
        if (direction.x > 0)
            _tempDirection = 1;
        else
            _tempDirection = -1;

        if (_animation == null || _animation.PlayedTime == _animationDuration)
        {
            _lineIndex = Mathf.Clamp(_lineIndex + _tempDirection, 0, _linePoint.childCount - 1);
            var point = _linePoint.GetChild(_lineIndex);
            _animation = transform.DoPositionX(point.transform.position.x, _animationDuration, Ease.InOutCirc).Play();
        }

    }
}
