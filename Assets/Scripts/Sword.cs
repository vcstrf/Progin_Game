using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private float timeBetweenAttack = 0.15f;
    [SerializeField] private Transform attackPos;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage = 5;
    [SerializeField] private Animator animator;

    public bool ShouldBeDamaging { get; private set; } = false;

    private List<IDamageable> iDamageables = new List<IDamageable>();

    private RaycastHit2D[] hits;
    public float attackTimeCounter;

    private void Update()
    {
        if (Input.GetMouseButton(0) && attackTimeCounter >= timeBetweenAttack)
        {
            attackTimeCounter = 0f;
            animator.SetTrigger("Attack");
        }
        attackTimeCounter += Time.deltaTime;
    }

    public IEnumerator DamageWhileSlashIsActive()
    {
        if (sword.activeInHierarchy)
        {
            ShouldBeDamaging = true;

            while (ShouldBeDamaging)
            {
                hits = Physics2D.CircleCastAll(attackPos.position, attackRange, transform.right, 0f, attackableLayer);

                for (int i = 0; i < hits.Length; i++)
                {
                    IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                    if (iDamageable != null && !iDamageable.HasTakenDamage)
                    {
                        Debug.Log("Sword hit damageable object.");
                        iDamageable.Damage(damage);
                        iDamageable.HasTakenDamage = true;
                        iDamageables.Add(iDamageable);
                    }
                }

                yield return null;
            }

            ReturnAttackablesToDamageable();
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void ShouldBeDamagingToTrue()
    {
        ShouldBeDamaging = true;
    }
    public void ShouldBeDamagingToFalse()
    {
        ShouldBeDamaging = false;
    }
}
