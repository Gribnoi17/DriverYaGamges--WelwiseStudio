using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Tweens;
using Redcode.Moroutines;
using UnityEngine.Rendering;
using System;

[RequireComponent(typeof(Animator))]
public class PoliceCar : MonoBehaviour
{
    [SerializeField] private GameObject _shield;
    [SerializeField] private Transform _linePoint;
    [SerializeField] private float _wheelAngelsSpeed;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Speedometer _spd;


    private bool _isPlaying = true;
    private int _lineIndex = 1;
    private Playable _animation;
    private Animator animator;
    private bool shieldActive;

    private void Start()
    {
        _isPlaying= true;
        SwipeDetection.SwipeEvent += OnSwipe;     
        animator = GetComponent<Animator>();
    }

    private void OnSwipe(Vector2 direction)
    {
        MoveSwipe(direction);
        _isPlaying = false;
    }

    private void OnDestroy()
    {
        SwipeDetection.SwipeEvent -= OnSwipe;
    }



    private void Update()
    {
        MoveKeyboard(KeyCode.A, -1);
        MoveKeyboard(KeyCode.D, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Shield")
        {
            Destroy(collision.gameObject);
            StopCoroutine(ShieldController());
            StartCoroutine(ShieldController());
        }else if(collision.gameObject.tag == "SpeedBooster")
        {
            Destroy(collision.gameObject);
            StartCoroutine(_spd.SpeedBoosterController());
        }else if (collision.gameObject.tag == "Car")
        {
            //��� �������
            if(!shieldActive)
            {
                EventManager.OnPlayerDied();
                gameObject.SetActive(false);
                _isPlaying= false;
            }else
            {
                //������� ������ ������� ���������
                Destroy(collision.gameObject);
            }
        }
    }

    private IEnumerator ShieldController()
    {
        shieldActive = true;
        if (_shield.activeSelf == false)
            _shield.SetActive(true);
        StartCoroutine(_spd.SpeedUnBusterOrShielPickUp());
        yield return new WaitForSeconds(3f);
        _shield.SetActive(false);
        shieldActive = false;
        StopCoroutine(ShieldController());
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
 
        yield return new WaitForSeconds(0.04f);
        animator.SetBool("None", true);
        animator.SetBool("RightTurn", false);
        StopCoroutine(OnTurnRight());
    }

    IEnumerator OnTurnLeft()
    {
        animator.SetBool("LeftTurn", true);
        animator.SetBool("None", false);

        yield return new WaitForSeconds(0.04f) ;
        animator.SetBool("None", true);
        animator.SetBool("LeftTurn", false);
        StopCoroutine(OnTurnLeft());
    }
}
