using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour {

    public static DialougeManager instance;

    #region Singleton
    /*private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    } */
    #endregion Singleton

    public Text text; // 실제 출력되는 대화
    public SpriteRenderer rendererSprite; // 캐릭터 이미지
    public SpriteRenderer rendererDialougeWindow; // 대화창 일러스트

    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialougeWindows;

    private int count; //진행상황

    public Animator animSprite;
    public Animator animDialougeWindow;

    private Move Move;

    public bool talking = false;
    private bool keyActivated = false;

    // Use this for initialization
    void Start () {
        count = 0;
        text.text = "";
        listSentences = new List<string>();
        listSprites = new List<Sprite>();
        listDialougeWindows = new List<Sprite>();
        Move = FindObjectOfType<Move>();
	}
	
    public void ShowDialouge(Dialouge dialouge)
    {
        talking = true;

        for(int i =0; i < dialouge.sentences.Length; i++)
        {
            listSentences.Add(dialouge.sentences[i]);
            listSprites.Add(dialouge.sprites[i]);
            listDialougeWindows.Add(dialouge.dialougeWindows[i]);
        }

        animSprite.SetBool("Appear", true);
        animDialougeWindow.SetBool("Appear", true);
        StartCoroutine(StartDialougeCoroutine());
    }
    
    public void ExitDialouge()
    {
        text.text = "";
        count = 0;
        listSentences.Clear();
        listSprites.Clear();
        listDialougeWindows.Clear();
        animSprite.SetBool("Appear", false);
        animDialougeWindow.SetBool("Appear", false);
        talking = false;
    }

    IEnumerator StartDialougeCoroutine()
    {
        if(count > 0)
        {
            if (listDialougeWindows[count] != listDialougeWindows[count - 1])
            {
                animSprite.SetBool("Change", true);
                animDialougeWindow.SetBool("Appear", false);
                yield return new WaitForSeconds(0.2f);
                rendererDialougeWindow.GetComponent<SpriteRenderer>().sprite = listDialougeWindows[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                animDialougeWindow.SetBool("Appear", true);
                animSprite.SetBool("Change", false);
            }
            else
            {
                if (listSprites[count] != listSprites[count - 1])
                {
                    animSprite.SetBool("Change", true);
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                    animSprite.SetBool("Change", false);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);

                }
            }
        }
        else
        {
            rendererDialougeWindow.GetComponent<SpriteRenderer>().sprite = listDialougeWindows[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
        }

        keyActivated = true;

        yield return new WaitForSeconds(0.3f);

        for (int i =0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i]; // 1글자씩 출력.
            yield return new WaitForSeconds(0.01f);
        }
        
    }
	// Update is called once per frame
	void Update () {
        if (talking && keyActivated)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                count++;
                text.text = "";

                if (count == listSentences.Count)
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
