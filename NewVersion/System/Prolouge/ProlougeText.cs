using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProlougeText : MonoBehaviour
{
    public Text ProlougeContent; // 화면에 띄워질 TEXT

    List<Dictionary<string, object>> ProlougeData;

    private List<string> ContentString = new List<string>();
    int ContentLength = 0;

    // Start is called before the first frame update
    void Start()
    {
        ProlougeContent.text = "";

        StartCoroutine(ShowContent("Content"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene("Prologue");
        }
    }

    IEnumerator ShowContent(string FileName)
    {
        ProlougeData = CSVReader.Read(FileName);
        ContentLength = ProlougeData.Count;

        for (var i = 0; i < ContentLength; ++i)
        {
            ContentString.Add(ProlougeData[i]["CONTENT"].ToString());
        }

        for (var i = 0; i < ContentString.Count; ++i)
        {
            yield return new WaitForSeconds(3f);

            ProlougeContent.text += ContentString[i] + "\n\n";
        }

        SceneManager.LoadScene("Prologue");
    }
}
