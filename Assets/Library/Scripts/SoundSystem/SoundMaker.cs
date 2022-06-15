using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace StealthLib
{

    /*
     * Klasse welche auf dem Objekt implementiert werden soll, welcher Ger�usche absondert.
     * Diese Klasse besitzt methoden welche von Anderen Klassen aufgerufen werden k�nnen.
     * Diese Methoden spielen die Soundeffekte in der defenierten Audiosource ab.
     * 
     * @source: Die Audiosorce-Komponente auf der die Ger�sche abgespielt werden sollen
     */

    public class SoundMaker : MonoBehaviourPun
    {
        [SerializeField] private AudioSource source;

        /*
         * Die Gesamtlautst�rke die, das Objekt macht
         */
        public float Volume { 

            get {
                if (!source.isPlaying)
                {
                   return 0;
                }
                return source.volume; 
            } 
            private set {
                source.volume = value; 
            } 
        }



        /*
         * Methode welche ein 3D-Sound erstellt und dieses der AudioSource �bergibt
         * @SoundSO: Die SoundKlasse welche abgespielt werden soll
         */
        public void SetSoundSOAudio(SoundSO sound)
        {
            source.spatialBlend = 1;
            source.loop = sound.IsLoop;
            source.maxDistance = sound.MaxHearDistance;
            source.minDistance = sound.MinHearDistance;
            source.volume = sound.Volume;

            if (!source.isPlaying)
            {
                if (sound.Audio.Length > 1)
                {
                    source.clip = sound.GetRandomeAudio();
                }
                else
                {
                    source.clip = sound.Audio[0];
                }
                source.Play();
            }
        }

        /*
         * Methode welche ein AudioClip abspielt.
         * Hierbei ist zu achten das die 3D-Sound einstellungen im Audiosource voreingestellt werden m�ssen.
         * @SoundSO: Die SoundKlasse welche abgespielt werden soll
         */
        public void SetAudioClipAudio(AudioClip sound, float volume)
        {
            source.volume = volume;
            source.clip = sound;
            source.Play();
        }


        /*
         * Zum Stoppen des Gespielten Sound n�tig.
         * Stoppt die Audio wenn ein AudioClip spielt.
         */
        public void StopAudio()
        {
            if (source.isPlaying) source.Stop();
        }


        #region NetworkingSoundPlaying

        /*
         * Um h�rbare Ger�usche Remote h�rbar zu machen, k�nnen diese Methoden genutzt werden.
         * F�r offline Spiele reichen die Oberen Methoden "SetAudioCLipAudio" und "SetSoundSOAudio" aus
         */

        [PunRPC]
        public void SetAudioRemoteSoundSO(string soundname)
        {
            SoundSO sound = SoundHolder.Instance.GetSoundSOByName(soundname);
            if (sound == null) return;
            SetSoundSOAudio(sound);
        }

        [PunRPC]
        public void SetAudioRemoteAudioClip(string soundname, int volume)
        {
            AudioClip sound = SoundHolder.Instance.GetAudioCLipByName(soundname);
            if (sound == null) return;
            SetAudioClipAudio(sound, volume);
        }

        [PunRPC]
        public void StopAudioRemote()
        {
            StopAudio();
        }
        #endregion

    }
}
