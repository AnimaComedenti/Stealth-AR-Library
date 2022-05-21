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
        [SerializeField] private float radius = 0.5f;

        private bool hasStartedPlaying = false;

        void Update()
        {

            if (hasStartedPlaying)
            {
                if (source.isPlaying)
                {
                    photonView.RPC("SetLight", RpcTarget.All, true);
                }
                else
                {
                    PhotonNetwork.Destroy(gameObject);
                }
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            if (colliders.Length < 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag(playerTag))
                    {
                        Debug.Log("Player on it");
                        photonView.RPC("SetAudioRemote", RpcTarget.AllBuffered, "Running");
                        hasStartedPlaying = true;
                    }
                }
            }
        }


        [PunRPC]
        public void SetLight(bool isLigthActiv)
        {
            if (isLigthActiv)
            {
                lightSource.gameObject.SetActive(true);
            }
            else
            {
                lightSource.gameObject.SetActive(false);
            }
            
        }

    }
}

