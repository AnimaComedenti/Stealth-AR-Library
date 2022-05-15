using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class UIQuitButton : MonoBehaviour
    {
        public void DisconnectOnClick()
        {
            PhotonNetwork.Disconnect();
            Application.Quit();
        }
    }
}