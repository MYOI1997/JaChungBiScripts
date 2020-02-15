using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 2020.01.09 수정 */

/* 1. Monobehaviour를 상속받은 클래스는 new 키워드로 생성이 불가능하다. 
        1) 이를 대체하기 위하여 다음과 같은 방법이 있다.
            <1> Monobehaviour의 명령어를 사용하지 않을 경우 Monobehaviour를 삭제한다.
            <2> GameObject.AddComponent()를 이용한다.
*/

public class ChatDialouge /* : MonoBehaviour*/
{
    public List<string> Sentences = new List<string>();
    public List<Sprite> Expressionsprites = new List<Sprite>();
    public List<Sprite> DialougeImages = new List<Sprite>(); 
    
    public int Length;

    List<Dictionary<string, object>> StringData;

    public ChatDialouge(string FileName)
    {
        StringData = CSVReader.Read(FileName);

        Length = StringData.Count;

        for (var i = 0; i < Length; i++)
        {
            Expressionsprites.Add(GetCharacter(StringData[i]["NAME"].ToString(), StringData[i]["EXPRESSION"].ToString()));
            /* 표정 */

            Sentences.Add(StringData[i]["CHAT"].ToString());
            /* 대화 */

            DialougeImages.Add(GetDialougeImage(StringData[i]["NAME"].ToString()));
            /* 대화창 */
        }
    }

    Sprite GetCharacter(string CharacterName, string ExperessionName)
    {
        return Resources.Load<Sprite>("Ilust/" + CharacterName + "/" + ExperessionName);
    }

    Sprite GetDialougeImage(string CharacterName)
    {
        return Resources.Load<Sprite>("Ilust/ChatImage/" + CharacterName);
    }
}
