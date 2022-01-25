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
    Material TransitionMaterial;
    public TransitionType CurrentTransition;
    public PlayerState playerState;
    public Animator animator;
    Rigidbody2D rb2d;

    private void Awake()
    {
        instance = this;
        CurrentTransition = TransitionType.Light;
        playerState = PlayerState.neutral;
        TransitionMaterial = Srenderer.material;
        TransitionMaterial.SetColor("Light_Color", TransitionColors.LightColor);
        TransitionMaterial.SetColor("Dark_Color", TransitionColors.DarkColor);
        TransitionMaterial.SetFloat("Transition_Amount", 0f);
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            TransitionMaterial.DOFloat(1f, "Transition_Amount", 1f);
            CurrentTransition = TransitionType.Dark;
        }
        else
        {
            TransitionMaterial.DOFloat(0f, "Transition_Amount", 1f);
            CurrentTransition = TransitionType.Light;
        }
    }

    public void TransitionComplete()
    {
        playerState = PlayerState.neutral;
    }
}


public enum TransitionType { Light, Dark }
public enum PlayerState {Damage, neutral, InTransition, Airborne }
