using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewUser
{
    public string Pos;
    public string Scene;

    public NewUser(string pos, string scene)
    {
        Pos = pos;
        Scene = scene;
    }
}

public class NewGamemanager : MonoBehaviour
{

    private FadeManager FM;

    public Button LoadButton;

    public static NewGamemanager GM;

    public GameObject Player;

    public NewUser Mycharacter;
    public static NewUser NewMy;

    public GameObject PlayerPrefab;
    public JsonData SaveData;

    public static string strFile;
    public static Vector3 Current;

    public GameObject Parent;

    public static bool LoadPossibleStatus;

    void Awake()
    {
        if (GM == null)
            GM = this;

        else if (GM != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        FM = FindObjectOfType<FadeManager>();

        FM.FadeIn();

        if(SceneManager.GetActiveScene().buildIndex >= 4)
        {
            if (LoadPossibleStatus == false)
            {
                Player = Instantiate(PlayerPrefab, new Vector2(-5.0f, -2.0f), Quaternion.identity) as GameObject;
                Player.transform.SetParent(Parent.transform);
            }
            else if (LoadPossibleStatus == true)
            {
                Player = Instantiate(PlayerPrefab, Current, Quaternion.identity) as GameObject;
                Player.transform.SetParent(Parent.transform);
                LoadPossibleStatus = false;
            }
        }
    }

    void Update()
    {
        strFile = Application.dataPath + "/Resources/User.json";
        FileInfo fileinfo = new FileInfo(strFile);

        if (LoadButton != null)
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
        string Pos = Player.transform.position.x + "," + Player.transform.position.y; ;
        string Scene = SceneManager.GetActiveScene().name;

        Mycharacter = (new NewUser(Pos, Scene));

        JsonData UserJson = JsonMapper.ToJson(Mycharacter);

        File.WriteAllText(Application.dataPath + "/Resources/User.json", UserJson.ToString());

        yield return null;
    }


    public void LoadBtn()
    {
        LoadPossibleStatus = true;

        FM.FadeOut();

        StartCoroutine(LoadUserData());
        StartCoroutine(CreateCharacter());
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
        catch (FileNotFoundException ex)
        {
            Debug.Log("로드 파일이 없습니다.");
        }

        yield return null;
    }

    private static void GetUserInfo(JsonData name)
    {
        GM.Mycharacter = new NewUser(name["Pos"].ToString(), name["Scene"].ToString());
    }

    IEnumerator CreateScene()
    {
        string SceneName = Mycharacter.Scene;

        SceneManager.LoadScene(SceneName);

        yield return null;
    }

    IEnumerator CreateCharacter()
    {
        if (Mycharacter != null)
        {

            Debug.Log("캐릭터 불러오기 시작");

            string[] tmpPosArray = Mycharacter.Pos.Split(',');

            Vector2 TmpPos = new Vector2(float.Parse(tmpPosArray[0]), float.Parse(tmpPosArray[1]));

            Current = new Vector2(float.Parse(tmpPosArray[0]), float.Parse(tmpPosArray[1]));

            Debug.Log("캐릭터의 포지션 값 " + float.Parse(tmpPosArray[0]) + " " + float.Parse(tmpPosArray[1]));

            Player.transform.position = Current;

            FM.FadeIn();

            yield return Current;
        }
        else
        {
            Debug.Log("로드 파일이 없습니다.");
            SceneManager.LoadScene("UI");
        }
    }
}