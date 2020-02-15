using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

/* 2020.01.09 수정 */

/* 1. 환경 설정 셋팅 값이 없을 경우 LoadSettings 함수를 호출하지 않도록 예외 처리 */

/* 2020.01.13 수정 */

/* 1. 환경설정에 대한 오브젝트들을 Awake() 함수에서 검색으로 할당하게 만듬 */

/* 2020.01.16 */
/* 1. 필요없는 변수 및 함수 삭제 */

/* 2020.01.18 */
/* 1. Resolution 중복 해상도 들어오는 이유 알아보기 */
/*      1) 비율이 16 : 9인 해상도만 추가하도록 수정 */
/*      2) 해상도 중복 제거 완료 */
/* 2. 메인 화면에서만 배경 음악을 재생하도록 수정 */

public class SettingManager : MonoBehaviour {

    private Toggle FullscreenToggle;
    private Dropdown ResolutionDropDown;
    private Dropdown TextureQualityDropDown;
    private Dropdown AntialiasingDropDown;
    private Dropdown VSyncDropDown;
    private Slider MusicVolumeSlider;
    private Button ApplyButton;
    private AudioSource MusicSource;

    public Resolution[] Resolutions;
    /* 해당 기기의 전체 해상도를 가지고 있는다 */

    public List<Resolution> WideResolutions;

    public GameSettings Settings;
    /* 해당 기기의 게임 설정을 json으로 관리한다. */

    void Awake()
    {
        FullscreenToggle = GameObject.Find("FullScreenToggle").GetComponent<Toggle>();
        ResolutionDropDown = GameObject.Find("ResolutionSize").GetComponent<Dropdown>();
        TextureQualityDropDown = GameObject.Find("TextureQuality").GetComponent<Dropdown>();
        AntialiasingDropDown = GameObject.Find("Antialiasing").GetComponent<Dropdown>();
        VSyncDropDown = GameObject.Find("vSync").GetComponent<Dropdown>();
        MusicVolumeSlider = GameObject.Find("MusicVolumeSlider").GetComponent<Slider>();
        ApplyButton = GameObject.Find("Apply").GetComponent<Button>();
        MusicSource = GameObject.Find("MusicTrack").GetComponent<AudioSource>();

        FullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        ResolutionDropDown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        TextureQualityDropDown.onValueChanged.AddListener(delegate { OnTextureQualityChange(); });
        AntialiasingDropDown.onValueChanged.AddListener(delegate { OnAntialiasingChange(); });
        VSyncDropDown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        MusicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        ApplyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 4)
        {
            MusicSource.Stop();
        }
        else
        {
            if(!MusicSource.isPlaying)
            {
                MusicSource.Play();
            }
        }
    }

    int GCD(int a, int b)
    {
        if(b == 0)
        {
            return a;
        }
        else
        {
            return GCD(b, a % b);
        }
    }

    void OnEnable()
    {
        Settings = new GameSettings();
        WideResolutions = new List<Resolution>();

        Resolutions = Screen.resolutions;

        foreach (Resolution resolution in Resolutions)
        {
            var MAX = GCD(resolution.width, resolution.height);

            if ((resolution.width / MAX == 16) && (resolution.height / MAX == 9)) // 비율이 16 : 9인 경우만 해상도 옵션에 추가
            {
                WideResolutions.Add(resolution);
                ResolutionDropDown.options.Add(new Dropdown.OptionData(resolution.ToString()));
            }
        }

        Debug.Log(WideResolutions.Count);

        LoadSettings();
    }

    public void OnFullscreenToggle()
    {
        Settings.Fullscreen = Screen.fullScreen = FullscreenToggle.isOn;

    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(WideResolutions[ResolutionDropDown.value].width, WideResolutions[ResolutionDropDown.value].height, FullscreenToggle.isOn);
        Settings.ResolutionIndex = ResolutionDropDown.value;
    }

    public void OnAntialiasingChange()
    {
        QualitySettings.antiAliasing = Settings.Antialiasing = (int)Mathf.Pow(2f, AntialiasingDropDown.value);
    }

    public void OnTextureQualityChange()
    {
        QualitySettings.masterTextureLimit = Settings.TextureQuality = TextureQualityDropDown.value;

    }

    public void OnVSyncChange()
    {
        QualitySettings.vSyncCount = Settings.VSync = VSyncDropDown.value;
    }

    public void OnMusicVolumeChange()
    {
        MusicSource.volume = Settings.MusicVolume = MusicVolumeSlider.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(Settings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesettings.json"))
        {
            Settings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

            MusicVolumeSlider.value = Settings.MusicVolume;
            AntialiasingDropDown.value = Settings.Antialiasing;
            VSyncDropDown.value = Settings.TextureQuality;
            TextureQualityDropDown.value = Settings.TextureQuality;
            ResolutionDropDown.value = Settings.ResolutionIndex;
            FullscreenToggle.isOn = Settings.Fullscreen;
            Screen.fullScreen = Settings.Fullscreen;

            ResolutionDropDown.RefreshShownValue();
        }
        else
        {
            MusicVolumeSlider.value = 1.0f;
            ResolutionDropDown.value = Resolutions.Length;
            FullscreenToggle.isOn = true;
            Screen.fullScreen = true;

            SaveSettings();
        }
    }
}
