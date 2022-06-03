using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace StealthDemo
{
    public class GameManager : MonoBehaviourPun
    {
        [SerializeField] private GameObject endGameUI;
        [SerializeField] private Text endGameMessage;
        [SerializeField] private Text winMassage;

        public static GameManager Instance
        {
            get { return _instance; }
        }

        public bool hasSeekerWon = false;
        public bool hasHiderWon = false;

        private static GameManager _instance = null;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            //CheckLivingHiders();

            if (hasSeekerWon)
            {
                photonView.RPC("SeekerWon", RpcTarget.All);
            }
            else if (hasHiderWon)
            {
                photonView.RPC("HiderWon", RpcTarget.All);
            }
        }

        /*
        private void CheckLivingHiders()
        {
            HiderPlayerController[] hiders = FindObjectsOfType<HiderPlayerController>();

            if (hiders.Length <= 0 && SeekerPlacementIndicator.Instance.isLevelPlaced && PhotonNetwork.PlayerList.Length >= 2) hasHiderWon = true; 
        }*/

        [PunRPC]
        private void HiderWon()
        {
            winMassage.text = "Hider has won!";
            endGameUI.SetActive(true);
            SetCursorVisble();
        }

        [PunRPC]
        private void SeekerWon()
        {
            winMassage.text = "Seeker has won!";
            endGameUI.SetActive(true);
            SetCursorVisble();
        }

        private void SetCursorVisble()
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
