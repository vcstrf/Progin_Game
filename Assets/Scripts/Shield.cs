using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
/*    [SerializeField] private float cooldown;
    [SerializeField] private bool isCooldown;

    private Image shieldImage;
    private Movement player;

    private void Start()
    {
        shieldImage = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        isCooldown = true;
    }

    private void Update()
    {
        if (isCooldown)
        {
            shieldImage.fillAmount -= 1 / cooldown * Time.deltaTime;
            if (shieldImage.fillAmount <= 0 )
            {
                shieldImage.fillAmount = 1;
                isCooldown = false;
                player.shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetTimer()
    {
        shieldImage.fillAmount = 1;
    }*/
}
