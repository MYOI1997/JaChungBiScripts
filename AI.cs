using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 2020.01.15 수정 */
/* 1. 사용하지 않는 변수 및 함수 삭제 */

public class AI : MonoBehaviour
{
    public float MovePower = 1.0f; 

    Animator Animator; 
    Vector3 Movement;
    int MovementFlag = 0;
    bool IsTracing = false;
    GameObject TraceTarget;

    void Start()
    {
        Animator = gameObject.GetComponentInChildren<Animator>();
        StartCoroutine("ChangeMovement");
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.gameObject.tag == "Player")
        {
            TraceTarget = other.gameObject;
            StopCoroutine("ChangeMovement");
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            IsTracing = true;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
     
        if (other.gameObject.tag == "Player")
        {
            IsTracing = false;
            StartCoroutine("ChangeMovement");
        }
    }

    IEnumerator ChangeMovement()
    {
        MovementFlag = Random.Range(0, 3);

        yield return new WaitForSeconds(1f);

        StartCoroutine("ChangeMovement");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;
        var dist = "";

        if (IsTracing)
        {
            Vector3 playerPos = TraceTarget.transform.position;

            if (playerPos.x < transform.position.x)
            {
                dist = "Left";
            }
            else if (playerPos.x > transform.position.x)
            {
                dist = "Right";
            }
        }
        else
        {
            if (MovementFlag == 1)
            {
                dist = "Left";
            }
            else if (MovementFlag == 2)
            {
                dist = "Right";
            }
        }

        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position += moveVelocity * MovePower * Time.deltaTime;
    }
}
