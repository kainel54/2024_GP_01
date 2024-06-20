using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [SerializeField] private int _currentStageNumber;
    private Button _stagedBtn;

    private void Awake()
    {
        _stagedBtn = GetComponent<Button>();
        _stagedBtn.onClick.AddListener(LoadStage);
    }

    private void LoadStage()
    {
        string stageName = $"Stage{_currentStageNumber}";
        SceneManager.LoadScene(stageName);
    }
}
