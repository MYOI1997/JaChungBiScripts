using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 2020.01.15 */
/* 필요없는 변수 및 함수 삭제 */

public class CameraManager : MonoBehaviour {

    private Transform TargetCamera;

    Vector3 Velocity = Vector3.zero;

    public float SmoothTime = 0.15f;

    public bool YMaxEnabled = false;
    public float YMaxValue = 0;

    public bool YMinEnabled = false;
    public float YMinValue = 0;

    public bool XMaxEnabled = false;
    public float XMaxValue = 0;

    public bool XMinEnabled = false;
    public float XMinValue = 0;

    void FixedUpdate () {

        var RM = GameObject.Find("RespawnManager").GetComponentInChildren<PlayerSpawn>();
        TargetCamera = RM.Player.transform;

        Vector3 TargetCameraPos = TargetCamera.position;

        if (YMinEnabled && YMaxEnabled)
        {
            TargetCameraPos.y = Mathf.Clamp(TargetCamera.position.y, YMinValue, YMaxValue);
        }
        else if (YMinEnabled)
        {
            TargetCameraPos.y = Mathf.Clamp(TargetCamera.position.y, YMinValue, TargetCamera.position.y);
        }
        else if (YMaxEnabled)
        {
            TargetCameraPos.y = Mathf.Clamp(TargetCamera.position.y, YMinValue, TargetCamera.position.y);
        }

        if (XMinEnabled && XMaxEnabled)
        {
            TargetCameraPos.x = Mathf.Clamp(TargetCamera.position.x, XMinValue, XMaxValue);
        }
        else if (XMinEnabled)
        {
            TargetCameraPos.x = Mathf.Clamp(TargetCamera.position.x, XMinValue, TargetCamera.position.x);
        }
        else if (XMaxEnabled)
        {
            TargetCameraPos.x = Mathf.Clamp(TargetCamera.position.x, TargetCamera.position.x, XMaxValue);
        }

        TargetCameraPos.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, TargetCameraPos, ref Velocity, SmoothTime);
		
	}
}
