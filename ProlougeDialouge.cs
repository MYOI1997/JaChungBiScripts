using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* 2020.01.16 */
/* 1. 필요없는 변수 및 함수 삭제, text 관리할 방법 생각해보기 */

/* 2020.01.20 */
/* 1. CSV파일을 불러와 보여주는 방식으로 수정 ProlugeDialouge -> ProlougeText */

public class ProlougeDialouge : MonoBehaviour {

    public Text text;
    public Text text2;
    public Text text3;
    public Text text4;

    // Use this for initialization
    void Start () {
        text.text = "";
        StartCoroutine(ShowDialoouge());
	}

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene("Prologue");
        }
    }

    IEnumerator ShowDialoouge()
    {
        yield return new WaitForSeconds(2f);
        text.text = "김진국 대감 부부가 늦은 나이에 자식을 얻기 위해 노력해, 딸을 낳게 되는데 그 이름을 자청비라 짓고, 애지중지 키운다. " +
                    "그렇게 태어난 자청비는 갈수록 용모가 아름답고, 특히 기질이 대단히 활달해졌다.";
        yield return new WaitForSeconds(5f);
        text2.text = "어느 날, 자청비는 우연히 하늘에서 지상으로 공부를 하러 내려온 문도령을 만나게 된다. 한눈에 반한 자청비는 남장을 하고서 문도령을 따라 나가 동문 생활을 하게 된다.";
        yield return new WaitForSeconds(5f);
        text3.text = "문도령은 자청비와 함께 공부하면서 여자가 아닌지 의심을 하게 되지만, 자청비는 항상 좋은 꾀를 떠올려 의심을 벗어난다. 그러길 3년, 문도령에게 하늘에서 결혼을 위해 하늘로 돌아오라는 편지가 온다.";
        yield return new WaitForSeconds(5f);
        text4.text = "다급함을 느낀 자청비는 자신이 여자 임을 밝히고 문도령에게 고백을 한다. 그에 문도령은 결혼을 약속하고 하늘로 올라간다. 하지만, 그 이후 하늘로 올라간 문도령에서 아무런 소식도 들을 수 없었다.";
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Prologue");
    }

}
