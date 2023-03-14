using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Tweens;
using Redcode.Moroutines;

[RequireComponent(typeof(Animator))]
public class PoliceCar : MonoBehaviour
{
    
    [SerializeField] private Transform _linePoint;
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private float _wheelAngelsSpeed;
    [SerializeField] private float _animationDuration;

    private bool _isPlaying = true;
    private int _lineIndex = 1;
    private Playable _animation;
    private Animator animator;
    private void Start()
    {
        SwipeDetection.SwipeEvent += OnSwipe;

        Moroutine.Run(RotateWheelsEnumerable());
     
        animator = GetComponent<Animator>();
    }

    private void OnSwipe(Vector2 direction)
    {
        MoveSwipe(direction);      
    }

    private void Update()
    {
        MoveKeyboard(KeyCode.A, -1);
        MoveKeyboard(KeyCode.D, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        _isPlaying = false;
    }

    private IEnumerable RotateWheelsEnumerable()
    {
        while (_isPlaying)
        {
            foreach (var wheel in _wheels)
            {
                wheel.rotation *= Quaternion.AngleAxis(_wheelAngelsSpeed * Time.deltaTime, Vector3.right);
            }
            yield return null;
        }
    }

    private void MoveKeyboard(KeyCode key, int direction)
    {
        if ((_animation == null || _animation.PlayedTime == _animationDuration) && Input.GetKeyDown(key))
        {
            if (direction > 0)
            {
                StartCoroutine(OnTurnRight());
            }
            else
            {
                StartCoroutine(OnTurnLeft());
            }
            _lineIndex = Mathf.Clamp(_lineIndex + direction, 0, _linePoint.childCount - 1);
            var point = _linePoint.GetChild(_lineIndex);
            _animation = transform.DoPositionX(point.transform.position.x, _animationDuration, Ease.InOutCirc).Play();
        }
    }

    private void MoveSwipe(Vector2 direction)
    {
        int _tempDirection;
        if (direction.x > 0)
        {
            _tempDirection = 1;
            StartCoroutine(OnTurnRight());
        }
        else
        {
            _tempDirection = -1;
            StartCoroutine(OnTurnLeft());
        }

        if (_animation == null || _animation.PlayedTime == _animationDuration)
        {
            _lineIndex = Mathf.Clamp(_lineIndex + _tempDirection, 0, _linePoint.childCount - 1);
            var point = _linePoint.GetChild(_lineIndex);
            _animation = transform.DoPositionX(point.transform.position.x, _animationDuration, Ease.InOutCirc).Play();
        }

    }

    IEnumerator OnTurnRight()
    {
        animator.SetBool("RightTurn", true);
        animator.SetBool("None", false);
 
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("None", true);
        animator.SetBool("RightTurn", false);
        StopCoroutine(OnTurnRight());
    }

    IEnumerator OnTurnLeft()
    {
        animator.SetBool("LeftTurn", true);
        animator.SetBool("None", false);

        yield return new WaitForSeconds(0.2f);
        animator.SetBool("None", true);
        animator.SetBool("LeftTurn", false);
        StopCoroutine(OnTurnLeft());
    }
}
