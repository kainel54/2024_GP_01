using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<FeedBack> _feedbackToPlay;

    private void Awake()
    {
        _feedbackToPlay = GetComponents<FeedBack>().ToList();
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        _feedbackToPlay.ForEach(f => f.CreateFeedback());
    }

    public void FinishFeedback()
    {
        _feedbackToPlay.ForEach(f => f.FinishFeedback());
    }
}
