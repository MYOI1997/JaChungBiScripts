using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

/* 20020.01.13 수정 */
/* 1. 비디오 플레이어 씬 전환 방식 수정 */

public class VideoPlayerManager : MonoBehaviour
{
    private VideoPlayer VideoSource;
    public string Main;

    void Start()
    {
        VideoSource = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        VideoSource.loopPointReached += OnMovieFinished;
    }

    void OnMovieFinished(VideoPlayer VideoSource)
    {
        VideoSource.Stop();
        SceneManager.LoadSceneAsync(Main);
    }
}
