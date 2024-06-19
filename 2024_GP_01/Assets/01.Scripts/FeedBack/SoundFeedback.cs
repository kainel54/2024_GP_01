using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFeedback : FeedBack
{
    [SerializeField] private SoundSO _soundData;

    public override void CreateFeedback()
    {
        SoundPlayer sound = PoolManager.Instance.Pop("SoundPlayer") as SoundPlayer;
        sound.PlaySound(_soundData);
    }

    public override void FinishFeedback()
    {

    }
}
