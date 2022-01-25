using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public TransitionType EnemyType;
    public TransitionColorsScriptable transitionColors;
    public int MaxHealth;
    int CurrentHealth;

    MeshRenderer Mrend;
    Material EnemyMaterial;


    private void Awake()
    {
        Mrend = GetComponent<MeshRenderer>();
        EnemyMaterial = Mrend.material;
        CurrentHealth = MaxHealth;
        SetEnemyColors();
    }

    public void TakeDamage(int damage, TransitionType BulletType)
    {
        if (BulletType == EnemyType)
        {
            FlashEffect("Flash_Amount");
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Death();
            }
        }
        else
        {
            FlashEffect("Inmunity_Amount");
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    void FlashEffect(string EffectName)
    {
        DOTween.KillAll();
        EnemyMaterial.SetFloat(EffectName, 1f);
        EnemyMaterial.DOFloat(0f, EffectName, 1f);
    }

    void SetEnemyColors()
    {
        if (EnemyType == TransitionType.Light) 
        {
            EnemyMaterial.SetColor("Enemy_Color", transitionColors.LightColor);
            EnemyMaterial.SetColor("Inmunity_Color", transitionColors.LightInmunityColor * 20f);
        }else
        {
            EnemyMaterial.SetColor("Enemy_Color", transitionColors.DarkColor);
            EnemyMaterial.SetColor("Inmunity_Color", transitionColors.DarkInmunityColor * 20f);
        }
    }
}
