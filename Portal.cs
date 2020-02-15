using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/* 2020.01.16 */
/* 1. 필요없는 변수 및 함수 삭제 */

/* 2020.01.18 */
/* 1. 포탈 코루틴 수정으로 FadeOut 함수가 종료되면 다음 씬 호출 */

public class Portal : MonoBehaviour {

    public GameObject Player;
    public string Name;

    private Convert Camera;

    public FadeManager FM;

    void Awake()
    {
        FM = GameObject.Find("System").transform.Find("FadeManager").gameObject.GetComponent<FadeManager>();
    }

    void start()
    {
        Camera = FindObjectOfType<Convert>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        FM.FadeOut(0.33f);

        yield return new WaitWhile(() => FM.color.a < 1.0f);

        SceneManager.LoadScene(Name);
    }
}
