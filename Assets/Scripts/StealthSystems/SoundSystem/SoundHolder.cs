using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundHolder : MonoBehaviour
{
    [SerializeField] List<SoundSO> sounds;

    private static SoundHolder _instance = null;
    public static SoundHolder Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public SoundSO GetSoundByName(string soundName)
    {
        foreach(SoundSO sound in sounds)
        {
            if (sound.AudioName == soundName) return sound;
        }
        return null;
    }
}
