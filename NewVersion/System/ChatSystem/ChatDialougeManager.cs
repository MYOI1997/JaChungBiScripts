using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 2020.01.08 수정 */

public class ChatDialougeManager : MonoBehaviour
{
    public Text ChatText; // 실제 출력되는 대화
    public SpriteRenderer ExpressionImage; // 캐릭터 이미지
    public SpriteRenderer DialougeImage; // 대화창 일러스트

    private List<string> SentencesList;
    private List<Sprite> SpritesList;
    private List<Sprite> DialoguesList;

    private int Count; //진행상황

    public Animator ExpressionAnim;
    public Animator DialougeAnim;

    private Move Move;

    public bool IsTalking = false;
    private bool IsKeyPressed = false;

    // Use this for initialization
    void Start()
    {
        Count = 0;
        ChatText.text = "";
        SentencesList = new List<string>();
        SpritesList = new List<Sprite>();
        DialoguesList = new List<Sprite>();
        Move = FindObjectOfType<Move>();
    }

    public void ShowDialouge(ChatDialouge Dialouge)
    {
        IsTalking = true;

        for(var i = 0; i < Dialouge.Length; i++)
        {
            SentencesList.Add(Dialouge.Sentences[i]);
            SpritesList.Add(Dialouge.Expressionsprites[i]);
            DialoguesList.Add(Dialouge.DialougeImages[i]);
        }

        ExpressionAnim.SetBool("Appear", true);
        DialougeAnim.SetBool("Appear", true);

        StartCoroutine(StartDialougeCoroutine());
    }

    public void ExitDialouge()
    {
        ChatText.text = "";
        Count = 0;

        SentencesList.Clear();
        SpritesList.Clear();
        DialoguesList.Clear();

        ExpressionAnim.SetBool("Appear", false);
        DialougeAnim.SetBool("Appear", false);

        IsTalking = false;
    }

    IEnumerator StartDialougeCoroutine()
    {
        if (Count > 0)
        {
            if (DialoguesList[Count] != DialoguesList[Count - 1])
            {
                ExpressionAnim.SetBool("Change", true);
                DialougeAnim.SetBool("Appear", false);
                yield return new WaitForSeconds(0.2f);

                DialougeImage.GetComponent<SpriteRenderer>().sprite = DialoguesList[Count];
                ExpressionImage.GetComponent<SpriteRenderer>().sprite = SpritesList[Count];

                DialougeAnim.SetBool("Appear", true);
                ExpressionAnim.SetBool("Change", false);
            }
            else
            {
                if (SpritesList[Count] != SpritesList[Count - 1])
                {
                    ExpressionAnim.SetBool("Change", true);
                    yield return new WaitForSeconds(0.1f);

                    ExpressionImage.GetComponent<SpriteRenderer>().sprite = SpritesList[Count];
                    ExpressionAnim.SetBool("Change", false);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);

                }
            }
        }
        else
        {
            DialougeImage.GetComponent<SpriteRenderer>().sprite = DialoguesList[Count];
            ExpressionImage.GetComponent<SpriteRenderer>().sprite = SpritesList[Count];
        }

        IsKeyPressed = true;

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < SentencesList[Count].Length; i++)
        {
            ChatText.text += SentencesList[Count][i]; // 1글자씩 출력.
            yield return new WaitForSeconds(0.01f);
        }

    }
    
    void Update()
    {
        if (IsTalking && IsKeyPressed)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                IsKeyPressed = false;
                Count++;
                ChatText.text = "";

                if (Count == SentencesList.Count)
                {
                    StopAllCoroutines();
                    ExitDialouge();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialougeCoroutine());
                }
            }
        }
    }
}
