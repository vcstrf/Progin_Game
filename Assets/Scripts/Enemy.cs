using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 20f;

    [SerializeField] private float currentHealth;

    [SerializeField] private GameObject deathEffect;

    [SerializeField] private float particleDestroyTime = 1f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private bool isPlayerHurt = false;

    public bool HasTakenDamage { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerHurt)
        {
            isPlayerHurt = true; 
            HealthManager.health--;
            StartCoroutine(GetHurt());
        }
    }

    private IEnumerator GetHurt()
    {
        yield return new WaitForSeconds(1.3f);
        isPlayerHurt = false;
    }

    public void Damage(float damageAmount)
    {
        HasTakenDamage = true;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, particleDestroyTime);
            Destroy(gameObject);
        }
    }
}
