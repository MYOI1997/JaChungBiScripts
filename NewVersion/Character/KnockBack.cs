using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 2020.01.14 수정 */
/* 필요 없는 변수 및 함수 삭제 */

public class KnockBack : MonoBehaviour
{
    PlayerManager PM;
    Animator Animator;
    Rigidbody2D RigidBody;

    float BeHitTime = 0;

    // Use this for initialization
    void Start()
    {
        RigidBody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
        PM = gameObject.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PM.CurrentHealth <= 0)
        {
            PM.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RigidBody.velocity = Vector2.zero;
        Vector2 attackedVelocity = Vector2.zero;

        if (other.gameObject.tag == "Enemy" && BeHitTime == 0.0f) 
        {
            Animator.SetTrigger("GetHit");
            PM.CurrentHealth -= 1;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            BeHitTime += 0.008f;

            Debug.Log(BeHitTime);
        }

        if (BeHitTime >= 1.0f)
        {
            Animator.SetTrigger("GetHit");
            PM.CurrentHealth -= 1;

            BeHitTime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BeHitTime = 0.0f;
    }
}
