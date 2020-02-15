using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

/* 2020.01.16 */
/* 1. 필요없는 변수 및 함수 삭제 */

public class StartGameButton : MonoBehaviour {

    public static string StrFile;

    public Button LoadBtn;
    public GameObject Setting;

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1f;
        Setting.SetActive(false);
    }

    void Update()
    {
        StrFile = Application.dataPath + "/Resources/User.json";
        FileInfo fileinfo = new FileInfo(StrFile);

      if (fileinfo.Exists)
        {
            LoadBtn.interactable = true;
        }
        else
        {
            LoadBtn.interactable = false;
        }
    }

    public void ChangeGameScene()
    {
        SceneManager.LoadScene("StoryBoard");
    }

    public void SceneLoad()
    {
        SceneManager.LoadScene("Loading");
    }

    public void LoadSetting()
    {
        Setting.SetActive(true);
    }

    public void Apply()
    {
        Setting.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
