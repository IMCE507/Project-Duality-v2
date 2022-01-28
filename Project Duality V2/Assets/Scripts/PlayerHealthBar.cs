using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField]
    PlayerHealthDataSO PlayerHealthValue;

    [SerializeField]
    Image EffectBar;

    [SerializeField]
    Image FillBar;

    Tween CurrentTween;

    private void OnEnable()
    {
        PlayerHealthValue.OnHealthChange += SetHealth;
    }

    private void OnDisable()
    {
        PlayerHealthValue.OnHealthChange -= SetHealth;
    }


    public void SetHealth(float CurrentHealth, float MaxHealth)
    {
        float HealthPosition = CurrentHealth / MaxHealth;
        FillBar.fillAmount = Mathf.Clamp01(HealthPosition);
        CurrentTween?.Kill();
        CurrentTween = EffectBar.DOFillAmount(FillBar.fillAmount, 0.3f).SetDelay(1f);
    }    

}
