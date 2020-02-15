using UnityEngine;
using System.Collections;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject Player;
    private GameObject Parent;

    public Vector2 StartPosition; // 캐릭터가 시작할 위치
    private Vector2 LoadPosition; // 캐릭터를 불러올 위치

    public GameObject PlayerPrefab;

    private Gamemanager GM;
    private FadeManager FM;

    void Awake()
    {
        if(Player != null)
        {
            Player = GameObject.Find("Player").gameObject.GetComponent<GameObject>();
        }

        Parent = GameObject.Find("Parent").gameObject.GetComponent<GameObject>();
        GM = GameObject.Find("Game Manager").gameObject.GetComponent<Gamemanager>();
        FM = GameObject.Find("System").transform.Find("FadeManager").gameObject.GetComponent<FadeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 4)
        {
            if (GM.LoadBool == false)
            {
                Debug.Log("새로 시작");
                Player = Instantiate(PlayerPrefab, StartPosition, Quaternion.identity) as GameObject;
                SetParent();
                GM.Player = Player;

                FM.FadeIn();
            }
            else if (GM.LoadBool == true)
            {
                Debug.Log("불러오기");

                string Jsonstring = File.ReadAllText(Application.dataPath + "/Resources/User.json");
                JsonData UserData = JsonMapper.ToObject(Jsonstring);
                User LoadData = new User(UserData["Pos"].ToString(), UserData["Scene"].ToString());

                string[] PosiionString = LoadData.Pos.Split(',');
                LoadPosition = new Vector2(float.Parse(PosiionString[0]), float.Parse(PosiionString[1]));

                Player = Instantiate(PlayerPrefab, LoadPosition, Quaternion.identity) as GameObject;
                SetParent();
                GM.LoadBool = false;
                GM.Player = Player;

                FM.FadeIn();
            }
        }
    }

    void SetParent()
    {
        if (Parent != null)
        {
            Player.transform.SetParent(Parent.transform);
        }
    }


}
