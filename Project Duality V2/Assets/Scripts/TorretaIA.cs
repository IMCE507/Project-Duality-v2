using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaIA : MonoBehaviour
{
    [SerializeField]
    Transform ShootingPosition;

    [SerializeField]
    GameObject EnemyBulletPrefab;

    [SerializeField]
    Health HealthScript;

    [SerializeField]
    float ShootingForce;

    [SerializeField]
    float secondsToShoot;

    [SerializeField]
    int Damage;

    float TimeToShoot;

    // Start is called before the first frame update
    void Start()
    {
        TimeToShoot = 0f;
    }

    private void Update()
    {
        TimeToShoot += Time.deltaTime;

        if (HealthScript.CurrentHealth <= 0)
            return;


        if (TimeToShoot >= secondsToShoot) 
        {
            TimeToShoot = 0f;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        Color TransitionColor = ColorSelect();
        GameObject bullet = Instantiate(EnemyBulletPrefab, ShootingPosition.position, ShootingPosition.rotation);
        EnemyBullet bulletObj = bullet.GetComponent<EnemyBullet>();
        bulletObj.rb2D.AddForce(ShootingPosition.right * ShootingForce, ForceMode2D.Impulse);
        bulletObj.BulletType = HealthScript.EnemyType;
        bulletObj.BulletColor = TransitionColor;
        bulletObj.BulletDamage = Damage;
    }

    Color ColorSelect()
    {
        if (HealthScript.EnemyType == TransitionType.Light)
        {
            return new Color(38.04974f, 15.39601f, 0);
        }
        else
        {
            return new Color(21.323f, 0, 38.04974f);
        }
    }
}
