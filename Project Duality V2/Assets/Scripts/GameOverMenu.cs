using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameOverMenu : PlayQuitBTNS
{
    public static GameOverMenu instance;

    [SerializeField]
    CanvasGroup canvasGroup;

    [SerializeField]
    TextMeshProUGUI PointsText;

    private void Awake()
    {
        instance = this;
    }

    public void ShowGameOverScreen(int Points)
    {
        canvasGroup.DOFade(1f, 1f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        PointsText.text = Points.ToString();
        
    }


}
