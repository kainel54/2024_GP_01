using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/SoundSO")]
public class SoundSO : ScriptableObject
{
    public enum AudioTypes
    {
        SFX,Music
    }

    public AudioTypes audioType;
    public AudioClip clip;
    public bool loop = false;
    public bool randomizePicth = false;
     
    [Range(0, 1f)]
    public float randomPitchModifier = 0.1f;
    [Range(0, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;
}
