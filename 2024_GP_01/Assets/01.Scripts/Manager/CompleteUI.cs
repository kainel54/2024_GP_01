using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompleteUI : MonoBehaviour
{
    [SerializeField] private Button _stageSelectedBtn,_nextStageBtn;

    private void Awake()
    {
        _stageSelectedBtn.onClick.AddListener(HandleStageSelectedBtn);
        _nextStageBtn.onClick.AddListener(HandleNextStageSelectedBtn);
    }

    private void HandleNextStageSelectedBtn()
    {
        SceneManager.LoadScene("Title");
    }

    private void HandleStageSelectedBtn()
    {
        SceneManager.LoadScene("Title");
    }
}
