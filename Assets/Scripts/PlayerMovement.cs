using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Walk,
    Attack,
    Interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D characterRigidbody;
    private Vector3 change;
    private Animator animator;

    void Start()
    {
        currentState = PlayerState.Walk;
        animator = GetComponent<Animator>();
        characterRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.Attack)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.Walk)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.Attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.Walk;
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        characterRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }
}
