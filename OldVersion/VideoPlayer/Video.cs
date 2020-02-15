using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Video : MonoBehaviour {
    bool IsDone = false;
    float fTime = 0f;
    AsyncOperation async_operation;


	// Use this for initialization
	void Start () {
        StartCoroutine(StartLoad("UI"));
    }
	
	// Update is called once per frame
	void Update () {
        fTime += Time.deltaTime;

        if (fTime >= 5)
        {
            async_operation.allowSceneActivation = true;
        }

    }
    public IEnumerator StartLoad(string strSceneName)
    {
        async_operation = Application.LoadLevelAsync(strSceneName);
        async_operation.allowSceneActivation = false;

        if (IsDone == false)
        {
            IsDone = true;

            while (async_operation.progress < 0.9f)
            {
                yield return true;
            }
        }
    }
    
}
