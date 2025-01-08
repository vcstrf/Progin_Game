using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;
    [SerializeField] private float bulletDamage = 1f;
    [SerializeField] GameObject bulletEffect;
    [SerializeField] private float particleDestroyTime = 1f;

    private Rigidbody2D body;
    private List<IDamageable> iDamageables = new List<IDamageable>();

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        SetDestroyTime();
        SetStraightVelocity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            Debug.Log("Bullet hit damageable object.");
            iDamageable.Damage(bulletDamage);
            iDamageable.HasTakenDamage = true;
            iDamageables.Add(iDamageable);

            // Сбросим состояние после нанесения урона
            ReturnAttackablesToDamageable();
        }

        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            GameObject effect = Instantiate(bulletEffect, transform.position, Quaternion.identity);
            Destroy(effect, particleDestroyTime);
            Destroy(gameObject);
        }
    }

    private void ReturnAttackablesToDamageable()
    {
        foreach (IDamageable thingThatWasDamaged in iDamageables)
        {
            thingThatWasDamaged.HasTakenDamage = false;
            Debug.Log("Reset HasTakenDamage for: " + thingThatWasDamaged);
        }

        iDamageables.Clear();
    }

    private void SetStraightVelocity()
    {
        body.velocity = transform.right * normalBulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }
}
