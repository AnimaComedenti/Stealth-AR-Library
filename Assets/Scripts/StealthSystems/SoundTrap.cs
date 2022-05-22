using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class SoundTrap : MonoBehaviourPun
    {
        [SerializeField] private string playerTag;
        [SerializeField] private Light lightSource;
        [SerializeField] private AudioSource source;

        private bool hasStartedPlaying = false;

        void Update()
        {
            if (!photonView.IsMine) return;

            if (hasStartedPlaying)
            {
                if (!source.isPlaying)
                {
                    PhotonNetwork.Destroy(gameObject);
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(playerTag))
            {
                Debug.Log("Player on it");
                photonView.RPC("SetAudioRemote", RpcTarget.AllBuffered, "Running");
                photonView.RPC("SetLight", RpcTarget.All);
                hasStartedPlaying = true;
            }
        }

        [PunRPC]
        public void SetLight()
        {
          lightSource.gameObject.SetActive(true);
        }

    }
}

