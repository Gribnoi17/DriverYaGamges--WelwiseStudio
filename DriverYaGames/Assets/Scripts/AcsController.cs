using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class AcsController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float deadZone;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private TextMeshProUGUI _value;

    [Header("Coordinates")]
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    private Animator animator;
    private float _acceleration = 0;
    [Header("For watch")]
    [SerializeField] private Rigidbody _rB;
    [SerializeField] private Slider _testSlider;
    public void GetNumber(float accelerationInHtml)
    {
        _acceleration = accelerationInHtml / 10;
    }

    private void Awake()
    {
        Invoke(nameof(FindPlayer), 3f);
        //
    }

    private void FindPlayer()
    {
        _rB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        animator = _rB.GetComponent<Animator>();
    }

    void Update()
    {
        _acceleration = _testSlider.value;
        try
        {
            _value.text = _acceleration.ToString();
        }
        catch
        {
            print("acs text error");
        }
        if (_acceleration > deadZone && _rB.transform.position.x < maxX)
        {
            var dir = new Vector3(_acceleration * speed, 0f, 0f);
            _rB.transform.position += dir * Time.deltaTime;
            animator.SetBool("None", false);
            animator.SetBool("RightTurn", true);
            animator.SetBool("LeftTurn", false);

        }
        else if (_acceleration < -deadZone && _rB.transform.position.x > minX)
        {
            var dir = new Vector3(_acceleration * speed, 0f, 0f);
            _rB.transform.position += dir * Time.deltaTime;
            animator.SetBool("None", false);
            animator.SetBool("LeftTurn", true);
            animator.SetBool("RightTurn", false);

        }
        else
        {
            if (_rB == null)
                return;

            //_rB.velocity = new Vector3(0f, 0f, 0f);
                animator.SetBool("None", true);
                animator.SetBool("LeftTurn", false);
                animator.SetBool("RightTurn", false);
        }
    }
}
