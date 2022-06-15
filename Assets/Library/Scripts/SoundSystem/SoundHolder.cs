using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StealthDemo;

namespace StealthLib
{
    /*
     * Diese Klasse ist eine Singelton Klasse und enthält alle Geräusche die das Spiel ausgeben kann.
     */
    public class SoundHolder : MonoBehaviour
    {
        [SerializeField] List<SoundSO> sounds;
        [SerializeField] List<AudioClipHolder> audioClips;

        #region AudioClipHolderClass
        [System.Serializable]
        public class AudioClipHolder
        {
            public AudioClip audio;
            public string name;

            public AudioClipHolder(AudioClip audio, string name)
            {
                this.audio = audio;
                this.name = name;
            }

            public AudioClip GetAudio()
            {
                return audio;
            }

            public string GetName()
            {
                return name;
            }
        }
        #endregion

        private static SoundHolder _instance = null;
        public static SoundHolder Instance
        {
            get {
                if (_instance == null) Debug.LogError("Singelton instance of SoundHolderClass is null");
                return _instance; 
            }
        }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.Log("SoundHolder already exists");
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        /*
         * Gibt das SoundSO-ScriptableObject anhand des Soundnamens zurück
         */
        public SoundSO GetSoundSOByName(string soundName)
        {
            foreach (SoundSO sound in sounds)
            {
                if (sound.AudioName == soundName) return sound;
            }
            Debug.LogError("Sound with Soundname: "+soundName+ "not Found");
            return null;
        }


        /*
         * Gibt den AudioClip anhand des Soundnamens zurück
         */
        public AudioClip GetAudioCLipByName(string soundName)
        {
            foreach (AudioClipHolder sound in audioClips)
            {
                if (sound.GetName() == soundName) return sound.GetAudio();
            }
            Debug.LogError("Sound with Soundname: " + soundName + "not Found");
            return null;
        }
    }
}
