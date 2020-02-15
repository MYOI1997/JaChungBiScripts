using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 2020.01.16 */
/* 1. 필요없는 변수 및 함수 삭제 */

public class Move : MonoBehaviour
{

    public float MovePower = 3f;
    public float JumpPower = 5f;

    public LayerMask LayerMask; // 통과가 불가능한 레이어를 설정할 때 사용함

    private BoxCollider2D BoxColider;

    Rigidbody2D RigidBody;
    SpriteRenderer Renderer;

    Animator PlayerAnimator;
    Vector3 PlayerPosition;
    Vector3 PlayerMovement;

    bool IsJumping = false;
    bool IsGrounded = true;
    static bool IsMoving = false;

    // Use this for initialization
    void Start()
    {
        BoxColider = gameObject.GetComponent<BoxCollider2D>();
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
        Renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        PlayerAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsMoving)
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                PlayerAnimator.SetInteger("Move", 0);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                PlayerAnimator.SetInteger("Move", 1);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                PlayerAnimator.SetInteger("Move", 1);
            }

            if (Input.GetButtonDown("Jump") && IsGrounded == true)
            {
                IsJumping = true;
            }

            if (IsJumping)
            {
                Jump();
                PlayerAnimator.SetTrigger("SetJump");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsMoving)
        {
            CharacterMovement();
            Jump();
        }
    }

    void CharacterMovement()
    {
        Vector3 MoveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            MoveVelocity = Vector3.left;
            Renderer.flipX = true;

        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            MoveVelocity = Vector3.right;
            Renderer.flipX = false;

        }

        transform.position += MoveVelocity * MovePower * Time.deltaTime;
    }

    public void Movement(string dir)
    {
        if (dir == "right")
        {
            Renderer.flipX = false;
            StartCoroutine(OrderMove(Vector3.right));       
        }
        if (dir == "left")
        {
            Renderer.flipX = true;
            StartCoroutine(OrderMove(Vector3.left));
        }
    }

    IEnumerator OrderMove(Vector3 MoveVelocity)
    {
        for (int i = 0; i < 100; i++)
        {
            transform.position += MoveVelocity * MovePower * 0.007f;//0.007f;

            PlayerAnimator.SetInteger("Move", 1);

            yield return new WaitUntil(() => PlayerAnimator);

            PlayerAnimator.SetInteger("Move", 0);
        }
    }

    void Jump()
    {
        if (!IsJumping)
            return;

        if (IsGrounded == true)
        {
            IsGrounded = false;
            RigidBody.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, JumpPower);
            RigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
            IsJumping = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsGrounded = true;
        }
    }

    // 프롤로그 애니메이션 제작에 사용
    public void NotMove()
    {
        IsMoving = true;
    }

    public void CanMove()
    {
        IsMoving = false;
    }
}