using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialouge : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] sentences; // 대화
    public Sprite[] sprites; // 캐릭터 이미지
    public Sprite[] dialougeWindows; // 대화창 이미지
}
