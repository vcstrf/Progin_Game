using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickingUp : MonoBehaviour
{
    private bool isColliding;
    [SerializeField] private GameObject sceneSwitcher;
    [SerializeField] private DetailManager dm;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Healing"))
        {
            if (isColliding) return;
            isColliding = true;
            if (HealthManager.health < 3)
            {
                HealthManager.health++;
                Destroy(other.gameObject);
            }
        }
        if (other.CompareTag("Detail"))
        {
            if (isColliding) return;
            isColliding = true;
            dm.detailCount++;
            dm.detailText.text = dm.detailCount.ToString() + " / 5";
            sceneSwitcher.SetActive(true);
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        isColliding = false;
    }
}
