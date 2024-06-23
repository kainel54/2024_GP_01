using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseSelectStage : MonoBehaviour
{
    [SerializeField] private Button _closeBtn;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _closeBtn = GetComponent<Button>();
        _closeBtn.onClick.AddListener(Close);
        _canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    private void Close()
    {
        _canvasGroup.DOFade(0, 0.5f);
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false; 
    }
}
