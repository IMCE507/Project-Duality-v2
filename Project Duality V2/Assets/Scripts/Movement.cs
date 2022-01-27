using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    PlayerManager playerManager;
    Rigidbody2D RB2D;

    [SerializeField]
    Animator animator;

    [SerializeField]
    Transform CircleOrigin;

    [SerializeField]
    float Radius;

    float MovementX;
    bool isgrounded;
    public LayerMask layer;
    

    public float MoveSpeed;
    public float JumpPower;
    public float JumpPull;


    // Start is called before the first frame update
    void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
        playerManager = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerManager.playerState)
        {
            case PlayerState.neutral:
                RunningJumping();
                break;

            case PlayerState.Airborne:
                RunningJumping();
                break;
        }
    }

    void RunningJumping()
    {
        isgrounded = Physics2D.OverlapCircle(CircleOrigin.position, Radius, layer);
        MovementX = Input.GetAxis("Horizontal");

        if (isgrounded)
        {
            playerManager.playerState = PlayerState.neutral;
            animator.SetBool("Jumping", false);
            if (Mathf.Abs(MovementX) > 0.1f)
            {
                animator.SetBool("IsRunning", true);
            }

            else
            {
                animator.SetBool("IsRunning", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RB2D.velocity = new Vector2(0, JumpPower);
            }

        }
        else
        {
            playerManager.playerState = PlayerState.Airborne;
            animator.SetBool("Jumping", true);
        }

        if (MovementX < -0.1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 180, 0);

        }
        else if (MovementX > 0.1)
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 0, 0);
        }


        if (RB2D.velocity.y < 0)
        {
            RB2D.velocity += new Vector2(0, JumpPull * Time.deltaTime);
        }

        RB2D.velocity = new Vector2(MovementX * MoveSpeed, RB2D.velocity.y);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(CircleOrigin.position, Radius);
    }


}


