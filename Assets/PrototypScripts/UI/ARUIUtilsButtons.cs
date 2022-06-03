using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StealthLib;

namespace StealthDemo
{
    public class ARUIUtilsButtons : MonoBehaviour
    {
        [SerializeField] private GameObject combatButtons;
        [SerializeField] private GameObject buildButtons;

        public void ToogelButtons()
        {
            UIToggler.Instance.ToggelUIButtons(combatButtons, buildButtons);
        }
    }
}
