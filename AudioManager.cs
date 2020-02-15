using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 2020.01.15 */
/* 1. 필요없는 변수 및 함수 삭제 */

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;
    private AudioSource Source;

    public float Volume;
    public bool Loop;

    public void SetSource(AudioSource AudioSource)
    {
        Source = AudioSource;
        Source.clip = Clip;
        Source.loop = Loop;

    }

    public void SetVolumn()
    {
        Source.volume = Volume;
    }

    public void Play()
    {
        Source.Play();
    }

    public void Stop()
    {
        Source.Stop();
    }

    public void SetLoop()
    {
        Source.loop = true;
    }

    public void SetLoopCancel()
    {
        Source.loop = false;
    }
}

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    [SerializeField]
    public Sound[] Sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            GameObject SoundObject = new GameObject("사운드 파일 이름: " + i + " = " + Sounds[i].Name);
            Sounds[i].SetSource(SoundObject.AddComponent<AudioSource>());
            SoundObject.transform.SetParent(transform);
        }
    }

    public void Play(string AudioName)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (AudioName == Sounds[i].Name)
            {
                Sounds[i].Play();
                return;
            }
        }
    }
    public void Stop(string AudioName)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (AudioName == Sounds[i].Name)
            {
                Sounds[i].Stop();
                return;
            }
        }
    }

    public void SetLoop(string AudioName)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (AudioName == Sounds[i].Name)
            {
                Sounds[i].SetLoop();
                return;
            }
        }
    }
    public void SetLoopCancel(string AudioName)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (AudioName == Sounds[i].Name)
            {
                Sounds[i].SetLoopCancel();
                return;
            }
        }
    }
    public void SetVolumn(string AudioName, float AudioVolume)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (AudioName == Sounds[i].Name)
            {
                Sounds[i].Volume = AudioVolume;
                Sounds[i].SetVolumn();
                return;
            }
        }
    }
}