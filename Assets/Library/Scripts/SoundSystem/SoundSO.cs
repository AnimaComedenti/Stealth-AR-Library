using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Bei diesem ScriptableObject können AudioClips mit zusätzlichen Audioeinstellungen erstellt werden.
     * Diese Einstellungen können beispielsweise für das Einstellen von 3D-Sounds an einem AudioSource-Objektes genutzt werden.
     */

    [CreateAssetMenu(menuName = "ScriptableObjects/SoundSO")]
    public class SoundSO : ScriptableObject
    {
        [SerializeField] private string audioName;
        [SerializeField] private AudioClip[] audio;
        [SerializeField] private float volume;
        [SerializeField] private float detectingVolume;
        [SerializeField] private float maxHearDistance;
        [SerializeField] private float minHearDistance;
        [SerializeField] private bool isLoop;

        public string AudioName => audioName;
        public AudioClip[] Audio => audio;
        public float Volume => volume;
        public float DetectingVolume => detectingVolume;
        public float MaxHearDistance => maxHearDistance;
        public float MinHearDistance => minHearDistance;
        public bool IsLoop => isLoop;

        private int lastNumber = 0;

        public AudioClip GetRandomeAudio()
        {
            int randomeNumber = 0;
            while (randomeNumber == lastNumber)
            {
                randomeNumber = Random.Range(0, audio.Length);
            }
            lastNumber = randomeNumber;
            return audio[randomeNumber];
        }
    }
}
