using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class AcsController : MonoBehaviour
{
    [SerializeField] private Rigidbody rg;
    [SerializeField] private float speed;
    [SerializeField] private float deadZone;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private TextMeshProUGUI _value;

    [Header("Coordinates")]
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    private Animator animator;
    private float _acceleration = 0;

    public void GetNumber(float accelerationInHtml)
    {
        _acceleration = accelerationInHtml;
    }

    private void Start()
    {
        animator = rg.GetComponent<Animator>();
    }

    void Update()
    {
        try
        {
            _value.text = Math.Truncate(_acceleration).ToString(); ;
        }
        catch
        {
            print("acs error");
        }
        if (_acceleration > deadZone && rg.transform.position.x < maxX)
        {
            var dir = new Vector3(_acceleration * speed, 0f, 0f);
            rg.velocity = dir; 
            animator.SetBool("None", false);
            animator.SetBool("RightTurn", true);
            animator.SetBool("LeftTurn", false);

        }
        else if (_acceleration < -deadZone && rg.transform.position.x > minX)
        {
            var dir = new Vector3(_acceleration * speed, 0f, 0f);
            rg.velocity = dir;
            animator.SetBool("None", false);
            animator.SetBool("LeftTurn", true);
            animator.SetBool("RightTurn", false);

        }
        else
        {
            rg.velocity = new Vector3(0f, 0f, 0f);
                animator.SetBool("None", true);
                animator.SetBool("LeftTurn", false);
                animator.SetBool("RightTurn", false);
        }
    }
}
