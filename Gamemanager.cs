using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* 2020.01.15 */
/* 1. 필요없는 변수 및 함수 삭제 */

/* 2020.01.17 */
/* 1. 캐릭터 생성에 관한 함수를 PlayerSpawn 스크립트로 분리 */

/* 2020.01.18 */
/* 1. 스크립트로 분리로 인한 사용하지 않는 변수 및 함수 삭제 */

public class User
{
    public string Pos;
    public string Scene;

    public User(string pos, string scene)
    {
        Pos = pos;
        Scene = scene;
    }
}

public class Gamemanager : MonoBehaviour {

    public static Gamemanager GM = null;

    public Button LoadButton;

    public GameObject Player; // 현재 생성된 캐릭터
    public User PlayerData; // 현재 캐릭터의 정보

    public string StrFile;

    public bool LoadBool = false; // 플레이어 생성에 관여하는 변수

    public GameObject Parent; // 플레이어의 부모 오브젝트

    void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
        else if(GM != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        LoadButton = GameObject.Find("System").transform.Find("Pause").transform.Find("Panel").transform.Find("Load").gameObject.GetComponent<Button>();
    }

    void Update()
    {
        StrFile = Application.dataPath + "/Resources/User.json";
        FileInfo fileinfo = new FileInfo(StrFile);

        if(LoadButton != null)
        {
            if (fileinfo.Exists)
            {
                LoadButton.interactable = true;
            }
            else
            {
                LoadButton.interactable = false;
            }
        }
    }

    public void SaveBtn()
    {
        StartCoroutine(SaveUserData());
    }

    IEnumerator SaveUserData()
    {
        Debug.Log("저장");

        string Pos = Player.transform.position.x + "," + Player.transform.position.y;
        string Scene = SceneManager.GetActiveScene().name;

        PlayerData = new User(Pos, Scene);

        JsonData UserJson = JsonMapper.ToJson(PlayerData);

        File.WriteAllText(Application.dataPath + "/Resources/User.json", UserJson.ToString());

        yield return null;
    }

    public void LoadBtn()
    {
        LoadBool = true;

        StartCoroutine(LoadUserData());
        StartCoroutine(CreateScene());
    }

    IEnumerator LoadUserData()
    {
        try
        {
            string Jsonstring = File.ReadAllText(Application.dataPath + "/Resources/User.json");
            Debug.Log(Jsonstring);

            JsonData UserData = JsonMapper.ToObject(Jsonstring);
            Debug.Log(UserData);

            GetUserInfo(UserData);
        }
        catch(FileNotFoundException ex)
        {
            Debug.Log("로드 파일이 없습니다.");
        }

        yield return null;
    }

    private static void GetUserInfo(JsonData name)
    {
        GM.PlayerData = new User(name["Pos"].ToString(), name["Scene"].ToString());
    }

    IEnumerator CreateScene()
    {
        string SceneName = PlayerData.Scene;

        SceneManager.LoadScene("Loading");

        yield return null;
    }
}


