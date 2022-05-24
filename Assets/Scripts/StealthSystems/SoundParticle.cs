using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthDemo
{
    public class SoundParticle : MonoBehaviourPun
    {

        [SerializeField] private string musicToPlay;
        [SerializeField] private AudioSource source;
        // Start is called before the first frame update
        void Start()
        {
            photonView.RPC("SetAudioRemoteSoundSO", RpcTarget.All, musicToPlay);
        }

        // Update is called once per frame
        void Update()
        {
            if (!source.isPlaying && photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}


