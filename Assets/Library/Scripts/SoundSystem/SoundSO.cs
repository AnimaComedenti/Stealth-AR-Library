using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthLib
{
    /*
     * Bei diesem ScriptableObject k?nnen AudioClips mit zus?tzlichen Audioeinstellungen erstellt werden.
     * Diese Einstellungen k?nnen beispielsweise f?r das Einstellen von 3D-Sounds an einem AudioSource-Objektes genutzt werden.
     */

    [CreateAssetMenu(menuName = "ScriptableObjects/SoundSO")]
    public class SoundSO : ScriptableObject
    {
        [SerializeField] private string audioName;
        [SerializeField] private AudioClip[] audio;
        [SerializeField] private float volume;
        [SerializeField] private float maxHearDistance;
        [SerializeField] private float minHearDistance;
        [SerializeField] private bool isLoop;

        public string AudioName => audioName;
        public AudioClip[] Audio => audio;
        public float Volume => volume;
        public float MaxHearDistance => maxHearDistance;
        public float MinHearDistance => minHearDistance;
        public bool IsLoop => isLoop;

        private int lastNumber = 0;

        public AudioClip GetRandomeAudio()
        {
            if (audio.Length == 1) return audio[0];
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
