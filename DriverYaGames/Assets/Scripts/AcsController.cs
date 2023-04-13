using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AcsController : MonoBehaviour
{
    [SerializeField] private Rigidbody rg;
    [SerializeField] private float speed;
    [SerializeField] private float deadZone;
    [SerializeField] private float rotateSpeed;

    [Header("Coordinates")]
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    private Animator animator;

    private void Start()
    {
        animator = rg.GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        if (acceleration.x > deadZone && rg.transform.position.x < maxX)
        {
            var dir = new Vector3(acceleration.x * speed, 0f, 0f);
            rg.velocity = dir; 
            animator.SetBool("None", false);
            animator.SetBool("RightTurn", true);
            animator.SetBool("LeftTurn", false);

        }
        else if (acceleration.x < -deadZone && rg.transform.position.x > minX)
        {
            var dir = new Vector3(acceleration.x * speed, 0f, 0f);
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
