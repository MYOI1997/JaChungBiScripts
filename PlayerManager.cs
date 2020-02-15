using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 2020.01.16 */
/* 1. 필요없는 변수 및 함수 삭제 */

/* 2020.01.17 */
/* 1. Move 스크립트를 확인해서 중복이 있으면 삭제하고 함수 역할 분리하기 */

public class PlayerManager : MonoBehaviour
{

    public int HealthMax = 4; //최대체력
    public int CurrentHealth;  //현재체력

    PauseMenu PauseMenu;
    FadeManager FM;

    private Move CharacterMovement;
    Rigidbody2D Rigidbody;

    Animator CharacterAnimator;


    // Use this for initialization
    void Start()
    {
        FM = FindObjectOfType<FadeManager>();
        PauseMenu = FindObjectOfType<PauseMenu>();
        CharacterMovement = FindObjectOfType<Move>();
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        CurrentHealth = HealthMax;
        CharacterAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        CharacterAnimator.SetBool("dead", true);
        FM.FadeOut();
        StartCoroutine(ActiveReStartMenu());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody.velocity = Vector2.zero;
        Vector2 attackedVelocity = Vector2.zero;

        if (other.gameObject.tag == "Trap")
        {
            CharacterAnimator.SetTrigger("GetHit");
            CurrentHealth -= 1;
            CharacterMovement.MovePower = 1f; 

            if (other.gameObject.transform.position.x > transform.position.x) 
                attackedVelocity = new Vector2(-0.5f, 0.1f);
            else
                attackedVelocity = new Vector2(0.5f, 0.1f);

            Rigidbody.AddForce(attackedVelocity, ForceMode2D.Impulse);

            StartCoroutine("InTrap");
        }

        if(other.gameObject.tag == "Drop")
        {
            CurrentHealth -= HealthMax;
        }        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopCoroutine("InTrap");  //코루틴 종료
        CharacterMovement.MovePower = 3f;  //속도 정상화
    }

    IEnumerator InTrap()  //함정 안에 있을시 1초마다 체력이 감소하는 코루틴
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f); // 1초에 한번 피격

            Debug.Log("InTrap!");
            CharacterAnimator.SetTrigger("GetHit");
            CurrentHealth -= 1;
        }
    }

    IEnumerator ActiveReStartMenu()
    {
        yield return new WaitForSeconds(2.0f);

        PauseMenu.DeadOption.SetActive(true);
        CharacterMovement.gameObject.SetActive(false);
    }
}
