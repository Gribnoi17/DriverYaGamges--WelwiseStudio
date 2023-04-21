using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class AcsController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rg;
    [SerializeField] private float speed;
    [SerializeField] private float deadZone;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private TextMeshProUGUI _valueForUnity;

    [Header("Coordinates")]
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    private Animator animator;
    private float _acceleration = 0;
    [Header("For watch")]
    [SerializeField]private Rigidbody _rB;

    public void GetNumber(float accelerationInHtml)
    {
        _acceleration = accelerationInHtml / 10;
    }

    private void Start()
    {
        foreach(Rigidbody r in _rg)
        {
            if (r.gameObject.activeSelf)
            {
                _rB = r;
            }
        }
        animator = _rB.GetComponent<Animator>();
    }

    void Update()
    {
      /*  Vector3 vec = Input.acceleration;
        try
        {
            _valueForUnity.text = vec.x.ToString();
        }
        catch
        {
            print("acs error");
        }*/
        try
        {
            _value.text = _acceleration.ToString();
        }
        catch
        {
            print("acs error");
        }
        if (_acceleration > deadZone && _rB.transform.position.x < maxX)
        {
            var dir = new Vector3(_acceleration * speed, 0f, 0f);
            _rB.velocity = dir; 
            animator.SetBool("None", false);
            animator.SetBool("RightTurn", true);
            animator.SetBool("LeftTurn", false);

        }
        else if (_acceleration < -deadZone && _rB.transform.position.x > minX)
        {
            var dir = new Vector3(_acceleration * speed, 0f, 0f);
            _rB.velocity = dir;
            animator.SetBool("None", false);
            animator.SetBool("LeftTurn", true);
            animator.SetBool("RightTurn", false);

        }
        else
        {
            _rB.velocity = new Vector3(0f, 0f, 0f);
                animator.SetBool("None", true);
                animator.SetBool("LeftTurn", false);
                animator.SetBool("RightTurn", false);
        }
    }
}
