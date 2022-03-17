using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthARLibrary
{
    public class ARUIButtonScript : MonoBehaviour
    {
        [SerializeField] private GameObject combatButtons;
        [SerializeField] private GameObject buildButtons;

        void Awake()
        {
            if (SystemInfo.deviceType != DeviceType.Handheld) gameObject.SetActive(false);
        }

        public void ToggelCombatMenu()
        {
           combatButtons.SetActive(true);
           buildButtons.SetActive(false);
        }

        public void ToggelBuildMenu()
        {

            buildButtons.SetActive(true);
            combatButtons.SetActive(false);
        }

        public void DisconnectOnClick()
        {
            PhotonNetwork.Disconnect();
            Application.Quit();
        }
    }
}

