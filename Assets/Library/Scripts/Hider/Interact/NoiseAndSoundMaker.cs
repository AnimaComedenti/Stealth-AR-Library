using System.Collections;
using UnityEngine;
using Photon.Pun;

namespace StealthLib
{
    /*
     * Klasse die von dem SoundMaker erbt und mit der Ausgabe von Lichtern erweitert.
     * 
     * lightRessource: Lichtquelle welche von dieser Klasse geändert werden soll
     * timeInterval: In welchen schritten ein Licht und ein Geräusch gemacht wird
     * soundAndLightDuration: Angabe wie Lange das Geräusch sowie das Licht aktiv sein soll
     */
    public class NoiseAndSoundMaker : SoundMaker
    {
        [SerializeField] private Light lightRessource;
        [SerializeField] private float timeInterval = 15;
        [SerializeField] private float soundAndLightDuration = 2;

        private float timer = 0;

        public bool SoundAndLightTimer()
        {
            timer += Time.deltaTime;

            if (timer >= (timeInterval + soundAndLightDuration))
            {
                timer = 0;
                return false;
            }

            if (timer >= timeInterval)
            {
                return true;
            }
            return false;
        }

        /*
         * Aktiviert und Deactiviert das Licht
         * 
         * shouldBeActivated: bool welcher bestimmt ob das Licht aktiviert werden soll
         * color: das Licht welches ausgegeben werden soll als Vector3
         * isRemote: ob das Licht über das Netzwerk sychnronisiert werden soll oder nicht
         */
        public void ActivateLight(bool shouldBeActivated, Vector3 color, bool isRemote = true)
        {
            if (isRemote)
            {
                photonView.RPC("SetLigthAndColor", RpcTarget.AllBuffered, color, shouldBeActivated);
                return;
            }

            SetLightAndColor(color, shouldBeActivated);
        }


        /*
         * Aktiviert und Deactiviert das Licht
         * 
         * shouldBeActivated: bool welcher bestimmt ob das Licht aktiviert werden soll
         * color: das Licht welches ausgegeben werden soll
         * isRemote: ob das Licht über das Netzwerk sychnronisiert werden soll oder nicht
         */
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