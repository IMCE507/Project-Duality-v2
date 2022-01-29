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

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        ShootBullet();
        yield return new WaitForSeconds(secondsToShoot);
        StartCoroutine(Shoot());
        if (HealthScript.CurrentHealth <= 0)
        {
            StopAllCoroutines();
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
            return HealthScript.transitionColors.LightColor;
        }
        else
        {
            return HealthScript.transitionColors.DarkColor;
        }
    }
}
