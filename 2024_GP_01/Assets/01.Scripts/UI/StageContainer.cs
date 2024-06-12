using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageContainer : MonoBehaviour
{
    private RectTransform _rect;
    [SerializeField] private Button[] _stageBtns;
    [SerializeField] private Button _leftBtn, _rightBtn,_stageBtn;
    private int _currentStage = 0;

    private void Awake()
    {
        _leftBtn.onClick.AddListener(LeftStage);
        _rightBtn.onClick.AddListener(RightStage);
        _stageBtn = _stageBtns[0];
        _stageBtn.onClick.AddListener(StageSelect);
        _rect = GetComponent<RectTransform>();
    }

    private void StageSelect()
    {
        string sceneName = $"Stage{_currentStage+1}";
        SceneManager.LoadScene(sceneName);
    }

    private void LeftStage()
    {
        if(_currentStage == _stageBtns.Length - 1)
        {
            _rightBtn.image.DOFade(1, 0.5f);
        }
        _rect.DOAnchorPosX(_rect.anchoredPosition.x + 1216.942f, 1f);
        _stageBtns[_currentStage].image.raycastTarget = false;
        _currentStage--;
        _stageBtn = _stageBtns[_currentStage];
        _stageBtn.onClick.AddListener(StageSelect);
        _stageBtns[_currentStage].image.raycastTarget = true;
        if(_currentStage == 0)
        {
            _leftBtn.image.DOFade(0,0.5f);
        }
    }

    private void RightStage()
    {
        
        _rect.DOAnchorPosX(_rect.anchoredPosition.x- 1216.942f, 1);
        _stageBtns[_currentStage].image.raycastTarget = false;
        _currentStage++;
        _stageBtn = _stageBtns[_currentStage];
        _stageBtn.onClick.AddListener(StageSelect);
        _stageBtns[_currentStage].image.raycastTarget = true;
        if (_currentStage == 1)
        {
            _leftBtn.image.DOFade(1, 0.5f);
        }
        if (_currentStage == _stageBtns.Length)
        {
            _rightBtn.image.DOFade(0,0.5f);
        }
    }

    public void StartChoose()
    {
        _rect.DOAnchorPosX(0, 1f).OnComplete(() =>
        {
            _rightBtn.image.DOFade(1, 0.5f);
        });
        _stageBtn = _stageBtns[0];
    }
}
