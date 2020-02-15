using UnityEngine;
using System.Collections;

/* 2020.01.15 */
/* 필요없는 변수 및 함수 삭제 */

public class Convert : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public Camera OverheadCamera;

    public void ShowOverheadView()
    {
        FirstPersonCamera.enabled = false;
        OverheadCamera.enabled = true;
    }

    public void ShowFirstPersonView()
    {
        FirstPersonCamera.enabled = true;
        OverheadCamera.enabled = false;
    }
}