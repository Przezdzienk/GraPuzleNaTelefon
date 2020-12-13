﻿using UnityEngine.Audio;
using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public float volume;
    [Range(0f,1f)]
    public float pitch;
    [Range(.1f, 3f)]

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
