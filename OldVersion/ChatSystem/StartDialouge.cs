using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDialouge : MonoBehaviour {
    [SerializeField]
    public Dialouge dialouge1;
    public Dialouge dialouge2;

    private Move move;

    private bool flag = true;

    private DialougeManager DM;
    private FadeManager FM;

    // Use this for initialization
    void Start()
    {
        DM = FindObjectOfType<DialougeManager>();
        FM = GameObject.Find("System").transform.Find("FadeManager").gameObject.GetComponent<FadeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && flag)
        {
            StartCoroutine(StartEvent());
        }
    }
    IEnumerator StartEvent()
    {
        move = FindObjectOfType<Move>();

        move.NotMove();

        move.Movement("right");
        move.Movement("right");
        yield return new WaitForSeconds(2.5f);

        DM.ShowDialouge(dialouge1);

        yield return new WaitUntil(() => !DM.talking);

        move.Movement("right");
        move.Movement("right");
        move.Movement("right");

        yield return new WaitForSeconds(2.3f);

        DM.ShowDialouge(dialouge2);
        yield return new WaitUntil(() => !DM.talking);
        move.CanMove();

        FM.FadeOut();

        SceneManager.LoadScene("Level 01");

        flag = false;

    }
}

 
