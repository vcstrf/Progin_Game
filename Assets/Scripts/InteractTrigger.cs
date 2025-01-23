using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] private GameObject trigger;
    [SerializeField] private GameObject text;
    private bool isPlayerInZone = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInZone = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInZone = false;
            text.SetActive(false);
        }
    }

    private void Update()
    {;
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.E))
        {
            trigger.SetActive(true);
        }
    }
}
