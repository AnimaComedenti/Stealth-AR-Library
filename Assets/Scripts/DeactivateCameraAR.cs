using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateCameraAR : MonoBehaviour
{
    public Camera usedCamera;
    private bool isDeactive = false;
    private void Update()
    {
        if (isDeactive) return;
        DeactivateCamera();
    }

    private void DeactivateCamera()
    {
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            usedCamera.enabled = false;
            isDeactive = true;
        }
    }
}
