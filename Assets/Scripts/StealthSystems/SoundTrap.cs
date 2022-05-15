using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class SoundTrap : MonoBehaviourPun
    {
        [SerializeField] private string playerTag;
        [SerializeField] private Light light;
        [SerializeField] private AudioSource source;

        // Update is called once per frame
        void Update()
        {

            if (source.isPlaying)
            {
                photonView.RPC("SetLight", RpcTarget.All, true);
            }
            else
            {
                photonView.RPC("SetLight", RpcTarget.All, false);
            }


            Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.y / 2);

            if (colliders.Length < 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag(playerTag))
                    {
                        photonView.RPC("SetAudioRemote", RpcTarget.All, "Running");
                    }
                }
            }
        }


        [PunRPC]
        public void SetLight(bool isLigthActiv)
        {
            if (isLigthActiv)
            {
                light.gameObject.SetActive(true);
            }
            else
            {
                light.gameObject.SetActive(false);
            }
            
        }

    }
}

