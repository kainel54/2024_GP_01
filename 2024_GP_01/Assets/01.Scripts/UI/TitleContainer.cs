using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TitleContainer : MonoBehaviour
{
    [SerializeField] private Button _startBtn, _exitBtn;
    [SerializeField] private StageContainer _stages;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _startBtn.onClick.AddListener(HandleStartEvent);
        _exitBtn.onClick.AddListener(HandleExitEvent);
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void HandleExitEvent()
    {
        Application.Quit();
    }

    private void HandleStartEvent()
    {
        _canvasGroup.DOFade(0, 1f).OnComplete(() =>
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _stages.StartChoose();
        });
    }
}
