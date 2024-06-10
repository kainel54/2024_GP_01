using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    public bool IsEnd { get; private set; }
    [SerializeField] private CanvasGroup _endCanvas;

    public void OnEndUI()
    {
        IsEnd = true;
        _endCanvas.DOFade(1f,0.5f).OnComplete(()=>Time.timeScale = 0);
    }
}
