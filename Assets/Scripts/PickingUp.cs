using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUp : MonoBehaviour
{
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
    }
}
