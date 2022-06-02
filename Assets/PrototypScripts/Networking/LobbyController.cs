using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class LobbyController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private byte roomSize = 2;
        [SerializeField] private string sceneName = "Mainscene";
        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                //Connect to server
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        //After callback join room
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        //If no room exists
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = roomSize });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Room joined");
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}
