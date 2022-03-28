using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthARLibrary;

public class ARUIUtilsButtons : MonoBehaviour
{
    [SerializeField] private GameObject combatButtons;
    [SerializeField] private GameObject buildButtons;
    private void Awake()
    {
        if (SystemInfo.deviceType != DeviceType.Handheld)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void ToogelButtons()
    {
        UIToggler.Instance.ToggelUIButtons(combatButtons,buildButtons);
    }

    public void DisconnectOnClick()
    {
        PhotonNetwork.Disconnect();
        Application.Quit();
    }
}
