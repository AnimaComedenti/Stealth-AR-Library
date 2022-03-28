using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSo : ScriptableObject
{
    [SerializeField] private string audioName;
    [SerializeField] private AudioClip audio;
    [SerializeField] private float volume;
    [SerializeField] private float maxHearDistance;
    [SerializeField] private float minHearDistance;
    [SerializeField] private bool isLoop;

    public string AudioName => audioName;
    public AudioClip Audio => audio;
    public float Volume => volume;
    public float MaxHearDistance => maxHearDistance;
    public float MinHearDistance => minHearDistance;
    public bool IsLoop => isLoop;
}
