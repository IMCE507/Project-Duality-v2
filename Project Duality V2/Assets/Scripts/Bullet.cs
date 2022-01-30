using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TransitionType BulletType;

    public int BulletDamage;

    public Rigidbody2D rb2D;

    [SerializeField]
    MeshRenderer mrend;

    Material Bulletmaterial;

    [HideInInspector]
    public Color BulletColor;

    private void Awake()
    {
        Bulletmaterial = mrend.material;
        Destroy(gameObject, 4f);
    }

    private void Start()
    {
        Bulletmaterial.SetColor("_EmissionColor", BulletColor * 40f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(("Enemy")))
        {
            var Enemy = collision.gameObject.GetComponent<Health>();
            Enemy.TakeDamage(BulletDamage, BulletType);
            Destroy(gameObject);
        }

        if (collision.CompareTag(("Shield")))
        {
            var EnemyShield = collision.gameObject.GetComponent<Shield>();
            EnemyShield.TakeDamage(BulletDamage, BulletType);
            Destroy(gameObject);
        }
    }

}


