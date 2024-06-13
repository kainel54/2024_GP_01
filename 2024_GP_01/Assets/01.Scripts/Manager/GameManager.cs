using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public bool IsPlay { get; internal set; }

    [SerializeField] private CanvasGroup _endCanvas;
    [SerializeField] private Image[] _stars;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private TextMeshProUGUI _startTimer; 
    private int _startTime = 3;

    private void Awake()
    {
        SceneManager.sceneLoaded += HandleLoadScene;
    }

    private void HandleLoadScene(Scene arg0, LoadSceneMode arg1)
    {
        StartCoroutine(StartTimer());
        _startTimer.DOFade(0, 0.1f);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleLoadScene; 
    }



    private IEnumerator StartTimer()
    {
        for(int i = 3; i > 0; i--)
        {
            _startTimer.text = $"{i}";
            _startTimer.DOFade(1, 0);
            yield return new WaitForSeconds(1f);
            _startTimer.DOFade(0, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }
        _startTimer.DOFade(1, 0);
        IsPlay = true;
        _startTimer.text = "Start";
        _startTimer.DOFade(0, 0.7f);


    }

    

    public void OnEndUI()
    {
        IsPlay = false;
        _time.text = string.Format("{0:D2}:{1:D2}", TimeManager.Instance._min, (int)TimeManager.Instance._sec);
        _endCanvas.DOFade(1f,0.5f).OnComplete(()=>Time.timeScale = 0);
        _endCanvas.interactable = true;
        _endCanvas.blocksRaycasts = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        TimeManager.Instance.gameObject.SetActive(false);
        if (TimeManager.Instance._min <= 1)
        {
            if (TimeManager.Instance._sec <= 30)
            {
                _stars[0].color = Color.yellow;
            }
            else if (TimeManager.Instance._sec <= 15)
            {
                for (int i = 0; i < 2; i++)
                {
                    _stars[i].color = Color.yellow;
                }
            }
            else if(TimeManager.Instance._sec == 0 || TimeManager.Instance._min == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    _stars[i].color = Color.yellow;
                }
            }
        }
    }

}
