using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private float currentHealth;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float particleDestroyTime = 1f;
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject trigger;

    void Start()
    {
        canvas.SetActive(false);
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        StartCoroutine(ShowAfterDelay(6f));
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

    IEnumerator ShowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.SetActive(true);
    }

    public void Damage(float damageAmount)
    {
        HasTakenDamage = true;

        currentHealth -= damageAmount;

        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, particleDestroyTime);
            Destroy(gameObject);
            Destroy(canvas);
            trigger.SetActive(true);
        }
    }
}
