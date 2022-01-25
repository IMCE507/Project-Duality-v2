using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shooting : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField]
    Transform ShootingPoint;

    [SerializeField]
    Animator animator;

    [SerializeField]
    float ShootingForce;

    public GameObject BulletPrefab;

    float NextFire;
    public float FireRate;

    float TimetoReturn;
    bool returned;
    public float ReturningTime;

    // Start is called before the first frame update
    void Start()
    {
        TimetoReturn = 0f;
        playerManager = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        switch(playerManager.playerState)
        {
            case PlayerState.neutral:
                {
                    shooting();
                }
                break;

            case PlayerState.Airborne:
                {
                    shooting();
                }
                break;
        }
    }

    void shooting()
    {
        TimetoReturn += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && (Time.time >= NextFire))
        {
            TimetoReturn = 0f;
            animator.SetLayerWeight(1, 1f);
            ShootBullet();
            returned = false;
        }

        if (TimetoReturn > 3f && !returned)
        {
            float weight = animator.GetLayerWeight(1);
            weight = Mathf.Lerp(weight, 0f, Time.deltaTime * ReturningTime);
            animator.SetLayerWeight(1, weight);

            if (weight <= 0.001f)
            {
                returned = true;
            }
        }
    }

    void ShootBullet()
    { 
        Color TransitionColor = ColorSelect();
        GameObject bullet = Instantiate(BulletPrefab, ShootingPoint.position, ShootingPoint.rotation);
        Bullet bulletObj = bullet.GetComponent<Bullet>();
        bulletObj.rb2D.AddForce(ShootingPoint.right * ShootingForce, ForceMode2D.Impulse);
        bulletObj.BulletType = playerManager.CurrentTransition;
        bulletObj.BulletColor = TransitionColor;
        NextFire = Time.time + 1f / FireRate;
    }

    Color ColorSelect()
    {
        if (playerManager.CurrentTransition == TransitionType.Light) 
        {
            return playerManager.TransitionColors.LightColor;
        }else
        {
            return playerManager.TransitionColors.DarkColor;
        }
    }
}
