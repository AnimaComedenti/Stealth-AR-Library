using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Utils
{
    public class Utils : MonoBehaviour
    {
        private PhotonView pv;
        private static Utils _instance;
        public static Utils Instance { get { return _instance; } }

        private void Start()
        {
            pv = GetComponent<PhotonView>();
        }

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
        public void DebugSmth(string message)
        {
            pv.RPC("DebugNetwork", RpcTarget.All, message);
        }

        [PunRPC]
        public void DebugNetwork(string message)
        {
            Debug.Log(message);
        }
    }
}

