using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace StealthLib
{
    public class NoiseAndSoundMaker : SoundMaker
    {
        [SerializeField] private Light lightRessource;
        [SerializeField] private float timeInterval = 15;
        [SerializeField] private float soundAndLightDuration = 2;

        private float timer = 0;

        public bool SoundAndLightTimer()
        {
            timer += Time.deltaTime;
            if (timer >= timeInterval)
            {
                return true;
            }
            if (timer >= (timeInterval + soundAndLightDuration))
            {
                timer = 0;
                return false;
            }
            return false;
        }


        public void ActivateLight(bool shouldBeActivated, Vector3 color, bool isRemote = true)
        {
            if (isRemote)
            {
                photonView.RPC("SetLigthAndColor", RpcTarget.AllBuffered, color, shouldBeActivated);
                return;
            }

            SetLightAndColor(color, shouldBeActivated);
        }

        public void ActivateLight(bool shouldBeActivated, Color color, bool isRemote = true)
        {
            Vector3 colorVector = new Vector3(color.r, color.g, color.b);
            if (isRemote)
            {
                photonView.RPC("SetLigthAndColor", RpcTarget.AllBuffered, colorVector, shouldBeActivated);
                return;
            }

            SetLightAndColor(colorVector, shouldBeActivated);
        }

        [PunRPC]
        public void SetLightAndColor(Vector3 color, bool activeState)
        {
            lightRessource.color = new Color(color.x, color.y, color.z, 255);
            lightRessource.gameObject.SetActive(activeState);
        }
    }
}