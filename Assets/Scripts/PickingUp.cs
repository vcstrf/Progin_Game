using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
    [SerializeField] private GameObject sceneSwitcher;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Healing"))
        {
            if (HealthManager.health < 3)
            {
                HealthManager.health += 1;
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("Detail"))
        {
            sceneSwitcher.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
