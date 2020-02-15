using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 2020.01.15 */
/* 1. 필요없는 변수 및 함수 삭제 */

public class FadeManager : MonoBehaviour {

    public SpriteRenderer BlackImage;
    public Color color;
    private WaitForSeconds WaitTime = new WaitForSeconds(0.01f);

    public void Awake()
    {
        BlackImage = GameObject.Find("FX - Black").GetComponent<SpriteRenderer>();
        BlackImage.color = Color.black;
        gameObject.SetActive(false);
    }

    public void FadeOut(float Speed = 0.003f)
    {
        Debug.Log("페이드 아웃" + SceneManager.GetActiveScene().name.ToString());
        gameObject.SetActive(true);
        StartCoroutine(FadeOutCoroutine(Speed));
    }

    IEnumerator FadeOutCoroutine(float Speed)
    {
        yield return new WaitForSeconds(1.0f);

        color = BlackImage.color;

        while (color.a < 1f)
        {
            color.a += Speed;
            BlackImage.color = color;
            yield return WaitTime;
        }
    }

    public void FadeIn(float Speed = 0.003f)
    {
        Debug.Log("페이드 인" + SceneManager.GetActiveScene().name.ToString());
        gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine(Speed));
    }

    IEnumerator FadeInCoroutine(float Speed)
    {
        yield return new WaitForSeconds(1.0f);

        color = BlackImage.color;

        while (color.a > 0f)
        {
            color.a -= Speed;
            BlackImage.color = color;
            yield return WaitTime;
        }
    }
}
