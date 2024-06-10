using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class TimeManager : MonoSingleton<TimeManager>
{
    [SerializeField] private TextMeshProUGUI _timerText;
    public float _sec { get; private set; }
    public int _min { get; private set; }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (GameManager.Instance.IsEnd) return;
        _sec += Time.deltaTime;

        _timerText.text = string.Format("{0:D2}:{1:D2}",_min,(int)_sec);
        if ((int)_sec > 59)
        {
            _sec = 0;
            _min++;
        }
    }
}
