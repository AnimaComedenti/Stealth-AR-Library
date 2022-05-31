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

            if (hasStartedPlaying)
            {
                if (!source.isPlaying)
                {
                    if(photonView.IsMine) PhotonNetwork.Destroy(gameObject);
                }

                return;
            }

            FindPlayer();
        }

        private void FindPlayer()
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(0.25f, 0.05f, 0.25f), Quaternion.identity);

            if(colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag(playerTag))
                    {
                        photonView.RPC("SetAudioRemoteSoundSO", RpcTarget.AllBuffered, "Running");
                        photonView.RPC("SetLight", RpcTarget.All);
                        hasStartedPlaying = true;
                    }
                }
            }
        }


        [PunRPC]
        public void SetLight()
        {
          lightSource.gameObject.SetActive(true);
        }

    }
}

