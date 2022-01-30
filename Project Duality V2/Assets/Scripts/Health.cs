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

    [HideInInspector]
    public int CurrentHealth;

    public float DissolveTime;
    public float HitFlashTime;
    public float InmunityFlashTime;

    [SerializeField]
    MeshRenderer Mrend;
    Material EnemyMaterial;

    Tween FlashTween;

    public int Points;

    private void Awake()
    {
        EnemyMaterial = Mrend.material;
        CurrentHealth = MaxHealth;
        SetEnemyColors();
    }

    public void TakeDamage(int damage, TransitionType BulletType)
    {
        if (BulletType == EnemyType)
        {
            FlashEffect("Flash_Amount", HitFlashTime);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Death();
            }
        }
        else
        {
            FlashEffect("Inmunity_Amount", InmunityFlashTime);
        }
    }

    private void Death()
    {
        Collider2D enemyColl = gameObject.GetComponent<Collider2D>();
        Rigidbody2D rb2 = enemyColl.attachedRigidbody;

        enemyColl.enabled = false;
        rb2.bodyType = RigidbodyType2D.Static;
        EnemyMaterial.DOFloat(1f, "Dissolve_Amount", DissolveTime).OnComplete((() => Destroy(gameObject)));
        PlayerManager.instance?.AddPoints(Points);
        
    }

    void FlashEffect(string EffectName, float Duration)
    {
        FlashTween?.Kill();
        EnemyMaterial.SetFloat(EffectName, 1f);
        FlashTween = EnemyMaterial.DOFloat(0f, EffectName, Duration);
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
