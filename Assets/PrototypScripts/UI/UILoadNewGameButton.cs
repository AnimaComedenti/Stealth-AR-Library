using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace StealthDemo
{
    public class UILoadNewGameButton : MonoBehaviour
    {
        [SerializeField] private string sceneName = "Mainscene";
        [SerializeField] private GameObject waitingText;
        [SerializeField] private PhotonView pv;

        private Text text;
        private float cnt = 0;
        private bool hasPressedOnce = false;

        private void Start()
        {
            text = waitingText.GetComponent<Text>();
        }
        public void RestartGameKlicked()
        {
            if (!hasPressedOnce)
            {
                pv.RPC("CountPressed",RpcTarget.AllBuffered);
                hasPressedOnce = true;
            }

            waitingText.SetActive(true);
        }

        private void FixedUpdate()
        {
            float currentPlayer = PhotonNetwork.CurrentRoom.PlayerCount;
            text.text = cnt + "/" + currentPlayer + " Player are Ready";

            if (cnt == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    pv.RPC("ReloadeScene", RpcTarget.OthersBuffered);
                    PhotonNetwork.LoadLevel(sceneName);
                }
            }
        }

        [PunRPC]
        public void CountPressed()
        {
            cnt++;
        }

        [PunRPC]
        public void ReloadeScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }

}

