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

    [SerializeField]
    PlayerHealthDataSO HealthData;

    [SerializeField]
    FloatSO PointsData;

    Tween FlashTween;

    public float KnockBackStrenght;
    public float InvulnerabilityTime;
    public float hitStopTime;

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
        SceneTransition.instace?.StartFadeIn();
    }

    private void Start()
    {
        PointsData.Value = 0;
        HealthData.MaxHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(1, TransitionType.Light, Vector2.zero);
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
            PlayerMaterial.SetColor("Inmunity_Color", Color.red);
            PlayerMaterial.DOFloat(1f, "Transition_Amount", 1f);
            CurrentTransition = TransitionType.Dark;
        }
        else
        {
            PlayerMaterial.SetColor("Inmunity_Color", Color.green);
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
        StopAllCoroutines();
        Physics2D.IgnoreLayerCollision(7, 6);
        rb2d.velocity = Vector2.zero;
        playerState = PlayerState.death;
        animator.SetLayerWeight(1, 0f);
        animator.SetTrigger("Dead");
        GameOverMenu.instance?.ShowGameOverScreen(PointsData.Value);
    }

    public void TakeDamage(int damage, TransitionType BulletType, Vector2 Direction)
    {
        if (BulletType == CurrentTransition)
        {
            playerState = PlayerState.Damage;
            FlashEffect("Flash_Amount", 0.2f);
            HealthData.CurrentHealth -= damage;
            HitStop.instance?.Stop(hitStopTime);
            StartCoroutine(Knockback(Direction * KnockBackStrenght));
            if (HealthData.CurrentHealth <= 0)
            {
                Death();
            }else
            {
                StartCoroutine(Invulnerability(InvulnerabilityTime));
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

    IEnumerator Knockback(Vector2 Direction)
    {
        Direction.y = 0;
        rb2d.AddForce(-Direction * KnockBackStrenght, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        playerState = PlayerState.neutral;
    }

    IEnumerator Invulnerability(float Duration)
    {
        PlayerMaterial.EnableKeyword("INVULNERABILITY");
        Physics2D.IgnoreLayerCollision(7, 6);
        yield return new WaitForSeconds(Duration);
        PlayerMaterial.DisableKeyword("INVULNERABILITY");
        Physics2D.IgnoreLayerCollision(7, 6, false);
    }

    public void AddPoints(int points)
    {
        PointsData.Value += points;
    }

}


public enum TransitionType { Light, Dark }
public enum PlayerState {Damage, neutral, InTransition, Airborne, death }
