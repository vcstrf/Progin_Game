using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinal : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);
        StartCoroutine(ShowAfterDelay(6.5f));
    }

    IEnumerator ShowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.SetActive(true);
    }
}
