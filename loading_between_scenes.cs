using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* 2020.01.16 */
/* 1. 필요없는 변수 및 함수 삭제 */

public class loading_between_scenes : MonoBehaviour
{
    public Slider LoadingSlider;

    bool IsDone = false;
    float LoadingTime = 0.0f;

    AsyncOperation Async_operation;
    private User LoadData;

    private void Awake()
    {
        string Jsonstring = File.ReadAllText(Application.dataPath + "/Resources/User.json");
        JsonData UserData = JsonMapper.ToObject(Jsonstring);
        LoadData = new User(UserData["Pos"].ToString(), UserData["Scene"].ToString());
    }

    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(StartLoad(LoadData.Scene.ToString()));
    }

    void Update()
    {
        LoadingTime += Time.deltaTime;
        LoadingSlider.value = LoadingTime;

        if (LoadingTime >= 3.0f)
        {
            Async_operation.allowSceneActivation = true;
            LoadingTime = 0;
        }
    }

    public IEnumerator StartLoad(string strSceneName)
    {
        if (IsDone == false)
        {
            IsDone = true;
            Async_operation = SceneManager.LoadSceneAsync(strSceneName);
            Async_operation.allowSceneActivation = false;

            while (Async_operation.progress < 0.9f)
            {
                LoadingSlider.value = Async_operation.progress;

                yield return true;

            }
        }
    }
}