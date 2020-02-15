using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* 2020.01.16 */
/* 1. 필요없는 변수 및 함수 삭제 */

/* 2020.01.17 */
/* 1. 각 메뉴 정적 할당에서 동적 할당으로 수정 */

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;
    public GameObject Settings;
    public GameObject DeadOption;

    private bool paused = false;

    // Use this for initialization
    void Start()
    {
        PauseUI = GameObject.Find("System").transform.Find("Pause").gameObject;
        Settings = GameObject.Find("System").transform.Find("Settings").gameObject;
        DeadOption = GameObject.Find("System").transform.Find("ReStart").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 4)
        {
            if (Input.GetButtonDown("Pause"))
            {
                Debug.Log("일시정지");

                paused = !paused;
            }
            if (paused)
            {
                PauseUI.SetActive(true);
                Time.timeScale = 0;
            }

            if (!paused)
            {
                PauseUI.SetActive(false);
                Settings.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Resume()
    {
        paused = !paused;
    }

    public void Restart()
    {
        Resume();
        string Scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Scene);
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingBtn()
    {
        Settings.SetActive(true);
    }

    public void OnApplicationQuit()
    {
        Resume();
        Application.Quit();
    }

    public void Apply()
    {
        Settings.SetActive(false);
    }
}
