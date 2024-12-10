using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isPlayerHurt = false;

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
}
