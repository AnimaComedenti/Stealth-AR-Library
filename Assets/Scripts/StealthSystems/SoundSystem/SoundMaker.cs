using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private SphereCollider sphereCollider;

    private float maxAudioDistance;

    private void Start()
    {
        sphereCollider.isTrigger = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (source.isPlaying)
        {
            CreateSphere();
        }
        else
        {
            ShrinkCollider();
        }
    }

    private void CreateSphere()
    {
        maxAudioDistance = source.maxDistance;
        sphereCollider.radius = maxAudioDistance;
    }

    private void ShrinkCollider()
    {
        maxAudioDistance = 1;
        sphereCollider.radius = 0.1f;
    }

    public void Play2DSound(SoundSo sound)
    {
        source.loop = sound.IsLoop;
        source.volume = sound.Volume;
        source.clip = sound.Audio;
        source.spatialBlend = 0;
        source.Play();
    }
    public void Play3DSound(SoundSo sound)
    {
        source.spatialBlend = 1;
        source.loop = sound.IsLoop;
        source.maxDistance = sound.MaxHearDistance;
        source.minDistance = sound.MinHearDistance;
        source.volume = sound.Volume;
        source.clip = sound.Audio;
        source.Play();
    }

    public void StopSound()
    {
        if (source.isPlaying) source.Stop();
    }

}
