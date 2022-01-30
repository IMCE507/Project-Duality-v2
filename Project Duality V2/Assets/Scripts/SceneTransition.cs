using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instace;

    public float FadeInTime;
    public float FadeOutTime;
    Image TransitionPanel;

    private void Awake()
    {
        instace = this;
        TransitionPanel = GetComponent<Image>();
        TransitionPanel.color = Color.black;
    }

    public void StartFadeIn(Action onFadeComplete)
    {
        TransitionPanel.DOFade(0, FadeInTime).OnComplete(() => 
        {
            TransitionPanel.gameObject.SetActive(false);
            onFadeComplete();

        });
    }

    public void StartFadeIn()
    {
        TransitionPanel.DOFade(0, FadeInTime).OnComplete(() =>
        {
            TransitionPanel.gameObject.SetActive(false);
        });
    }

    public void StartFadeOut(Action OnFadeComplete)
    {
        TransitionPanel.gameObject.SetActive(true);
        TransitionPanel.DOFade(1, FadeOutTime).OnComplete(() => OnFadeComplete());
    }
}
