using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Shield : MonoBehaviour
{
    [SerializeField]
    TransitionType ShieldType;

    [SerializeField]
    int MaxShieldHealth;

    int CurrentHealth;

    [SerializeField]
    MeshRenderer Mrenderer;
    Material ShieldMaterial;

    Tween FlashTween;

    private void Awake()
    {
        ShieldMaterial = Mrenderer.material;
        CurrentHealth = MaxShieldHealth;
        SetShieldColor();
    }


    public void TakeDamage(int Damage, TransitionType BulletType)
    {
        if (BulletType == ShieldType)
        {
            HitEffect();
            CurrentHealth -= Damage;
            if (CurrentHealth <= 0)
            {
                DestroyShield();
            }
        }
    }

    private void DestroyShield()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        ShieldMaterial.DOFloat(1f, "DissolveAmount", 0.3f);
    }

    void HitEffect()
    {
        FlashTween?.Kill();
        ShieldMaterial.SetFloat("HitEffectAmount", 1f);
        FlashTween = ShieldMaterial.DOFloat(0f, "HitEffectAmount", 0.25f);
    }

    void SetShieldColor()
    {
        if (ShieldType == TransitionType.Light)
        {
            ShieldMaterial.SetColor("FillColor", new Color(1f, 0.54321f, 0f, 0.2f));
        }
        else
        {
            ShieldMaterial.SetColor("FillColor", new Color(1f, 0, 0.7221112f, 0.2f));
        }
    }
}
