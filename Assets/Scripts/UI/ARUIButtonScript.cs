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
        [SerializeField] private GameObject confirmButtons;
        [SerializeField] private GameObject menuButtons;
        [SerializeField] private GameObject rotateButtons;


        void Awake()
        {
            if (SystemInfo.deviceType != DeviceType.Handheld)
            {
                gameObject.SetActive(false);
                return;
            }
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

        public void ToggelAiUIButtons()
        {

            if (confirmButtons.activeSelf)
            {
                confirmButtons.SetActive(false);
                menuButtons.SetActive(true);
            }
            else
            {
                confirmButtons.SetActive(true);
                menuButtons.SetActive(false);
            }
        }

        public void ToggelRotationMenu()
        {
            if (rotateButtons.activeSelf)
            {
                rotateButtons.SetActive(false);
                menuButtons.SetActive(true);
            }
            else
            {
                rotateButtons.SetActive(true);
                menuButtons.SetActive(false);
            }
        }

        public void DisconnectOnClick()
        {
            PhotonNetwork.Disconnect();
            Application.Quit();
        }
    }
}
