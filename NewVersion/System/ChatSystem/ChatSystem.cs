using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 2020.01.08 수정 */

public class ChatSystem : MonoBehaviour
{
    private ChatDialouge Dialouge1;
    private ChatDialouge Dialouge2;
    private FadeManager FM;

    private Move Move;

    private bool Flag = true;
    private ChatDialougeManager DM;

    void Start()
    {
        DM = FindObjectOfType<ChatDialougeManager>();
        FM = FindObjectOfType<FadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Flag)
        {
            StartCoroutine(StartEvent());
        }
    }
    IEnumerator StartEvent()
    {
        Move = FindObjectOfType<Move>();

        Move.NotMove();

        Move.Movement("right");
        Move.Movement("right");
        yield return new WaitForSeconds(2.5f);

        DM.ShowDialouge(new ChatDialouge("CHAT1"));

        yield return new WaitUntil(() => !DM.IsTalking);

        Move.Movement("right");
        Move.Movement("right");
        Move.Movement("right");

        yield return new WaitForSeconds(2.3f);

        DM.ShowDialouge(new ChatDialouge("CHAT2"));
        yield return new WaitUntil(() => !DM.IsTalking);
        Move.CanMove();

        FM.FadeOut();

        SceneManager.LoadScene("Level 01");

        Flag = false;

    }
}
