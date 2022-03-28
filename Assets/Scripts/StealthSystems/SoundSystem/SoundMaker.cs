using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SoundMaker : MonoBehaviour, IHearable
{
    [SerializeField] private AudioSource source;
    private PhotonView pv;

    private float wholeVolume;


    public void Play2DSound(SoundSO sound)
    {
        source.spatialBlend = 0;
        source.loop = sound.IsLoop;
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
            wholeVolume = sound.Volume;
            source.Play();
        }
    }

    public void Play3DSound(SoundSO sound)
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
            wholeVolume = sound.Volume;
            source.Play();
        }
    }

    public void StopSound()
    {
        if (source.isPlaying) source.Stop();
    }

    public float hearedVolume()
    {
        return wholeVolume;
    }
}
