using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public TransitionColorsScriptable TransitionColors;

    [SerializeField]
    SkinnedMeshRenderer Srenderer;
    Material PlayerMaterial;
    public TransitionType CurrentTransition;
    public PlayerState playerState;
    public Animator animator;
    Rigidbody2D rb2d;

    public int MaxHealth;
    int CurrentHealth;

    Tween FlashTween;

    private void Awake()
    {
        instance = this;
        CurrentTransition = TransitionType.Light;
        playerState = PlayerState.neutral;
        PlayerMaterial = Srenderer.material;
        PlayerMaterial.SetColor("Light_Color", TransitionColors.LightColor);
        PlayerMaterial.SetColor("Dark_Color", TransitionColors.DarkColor);
        PlayerMaterial.SetFloat("Transition_Amount", 0f);
        PlayerMaterial.SetColor("Inmunity_Color", TransitionColors.LightInmunityColor);
        rb2d = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1, TransitionType.Light);
        }

        switch(playerState)
        {
            case PlayerState.neutral:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    rb2d.velocity = Vector2.zero;
                    animator.SetLayerWeight(1, 0f);
                    animator.SetTrigger("Transition");
                    playerState = PlayerState.InTransition;
                }
                break;
        }
    }

    public void Transition()
    {
        if (CurrentTransition == TransitionType.Light)
        {
            PlayerMaterial.SetColor("Inmunity_Color", TransitionColors.LightInmunityColor);
            PlayerMaterial.DOFloat(1f, "Transition_Amount", 1f);
            CurrentTransition = TransitionType.Dark;
        }
        else
        {
            PlayerMaterial.SetColor("Inmunity_Color", TransitionColors.DarkInmunityColor);
            PlayerMaterial.DOFloat(0f, "Transition_Amount", 1f);
            CurrentTransition = TransitionType.Light;
        }
    }

    public void TransitionComplete()
    {
        playerState = PlayerState.neutral;
    }

    public void Death()
    {
        rb2d.velocity = Vector2.zero;
        playerState = PlayerState.death;
        animator.SetLayerWeight(1, 0f);
        animator.SetTrigger("Dead");

    }

    public void TakeDamage(int damage, TransitionType BulletType)
    {
        if (BulletType == CurrentTransition)
        {
            FlashEffect("Flash_Amount", 0.2f);
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Death();
            }
        }
        else
        {
            FlashEffect("Inmunity_Amount", 1f);
        }
    }

    void FlashEffect(string EffectName, float Duration)
    {
        FlashTween?.Kill();
        PlayerMaterial.SetFloat(EffectName, 1f);
        FlashTween = PlayerMaterial.DOFloat(0f, EffectName, Duration);
    }
}


public enum TransitionType { Light, Dark }
public enum PlayerState {Damage, neutral, InTransition, Airborne, death }
