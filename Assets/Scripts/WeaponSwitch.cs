using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject journal;
    [SerializeField] private GameObject detail;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gun.activeInHierarchy)
            {
                gun.SetActive(false);
                sword.SetActive(true);
            }
            else if (sword.activeInHierarchy) 
            {
                sword.SetActive(false);
                gun.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeInHierarchy)
            {
                menu.SetActive(true);
                detail.SetActive(false);
                journal.SetActive(false);
            }
            else 
            {
                menu.SetActive(false);
                detail.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!journal.activeInHierarchy)
            {
                journal.SetActive(true);
                detail.SetActive(false);
                menu.SetActive(false);
            }
            else
            {
                journal.SetActive(false);
                detail.SetActive(true);
            }
        }
    }
}
