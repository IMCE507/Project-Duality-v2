using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public TransitionType BulletType;

    public int BulletDamage;

    public Rigidbody2D rb2D;

    [SerializeField]
    MeshRenderer mrend;

    Material Bulletmaterial;


    public Color BulletColor;

    private void Awake()
    {
        Bulletmaterial = mrend.material;
        Destroy(gameObject, 4f);
    }

    private void Start()
    {
        Bulletmaterial.SetColor("FresnelColor", BulletColor * 40f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(("Player")))
        {
            Vector2 BulletDirection = (gameObject.transform.position - PlayerManager.instance.transform.position).normalized;
            PlayerManager.instance.TakeDamage(BulletDamage, BulletType, BulletDirection);
            Destroy(gameObject);
        }
    }

}
