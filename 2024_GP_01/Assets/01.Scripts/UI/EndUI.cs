using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    [SerializeField] private Button _selectStageBtn, _nextStageBtn;


    private void Awake()
    {
        _selectStageBtn.onClick.AddListener(SelectStageEvent);
        _nextStageBtn.onClick.AddListener(NextStageEvent);
    }

    private void NextStageEvent()
    {
        Debug.Log(SceneManager.loadedSceneCount);
    }

    private void SelectStageEvent()
    {
        SceneManager.LoadScene("Title");
    }
}
