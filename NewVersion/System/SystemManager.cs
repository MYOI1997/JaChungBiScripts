using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemManager : MonoBehaviour
{
    private static SystemManager System = null;
    private GameObject Menu;

    void Awake()
    {
        if(System == null)
        {
            System = this;
        }
        else if(System != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Menu = GameObject.Find("Menu").gameObject;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 2)
        {
            Menu.SetActive(false);
        }
        else
        {
            Menu.SetActive(true);
        }
    }
}
