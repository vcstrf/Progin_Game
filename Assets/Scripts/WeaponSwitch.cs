using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject sword;

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
    }
}
