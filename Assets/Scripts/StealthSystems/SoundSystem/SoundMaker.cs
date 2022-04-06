using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundMaker : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    private float wholeVolume = 0;

    private void Update()
    {
        if (!source.isPlaying)
        {
            source.volume = 0;
            wholeVolume = 0;
        }
    }

    public float Volume()
    {
        return wholeVolume;
    }

    public void SetAudio(SoundSO sound)
    {
        source.spatialBlend = 1;
        source.loop = sound.IsLoop;
        source.maxDistance = sound.MaxHearDistance;
        source.minDistance = sound.MinHearDistance;
        source.volume = sound.Volume;
        wholeVolume = sound.DetectingVolume;

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

    [PunRPC]
    public void SetAudioRemote(string soundname)
    {
        SoundSO sound = SoundHandler.Instance.GetSoundByName(soundname);
        if (sound == null)
        {
            Debug.Log("Soundname not found");
            return;
        }
        SetAudio(sound);
    }

    [PunRPC]
    public void StopAudio()
    {
        if (source.isPlaying) source.Stop();
    }

}
