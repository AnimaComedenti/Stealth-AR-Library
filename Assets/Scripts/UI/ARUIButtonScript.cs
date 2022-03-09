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

        void Start()
        {
            buildButtons.SetActive(true);
            combatButtons.SetActive(false);
        }
        public void ToggelCombatMenu()
        {
            if (combatButtons.activeSelf)
            {
                combatButtons.SetActive(false);
            }
            else
            {
                combatButtons.SetActive(true);
                buildButtons.SetActive(false);
            }
        }

        public void ToggelBuildMenu()
        {
            if (buildButtons.activeSelf)
            {
                buildButtons.SetActive(false);
            }
            else
            {
                buildButtons.SetActive(true);
                combatButtons.SetActive(false);
            }
        }

        public void DisconnectOnClick()
        {
            PhotonNetwork.Disconnect();
            Application.Quit();
        }
    }
}

