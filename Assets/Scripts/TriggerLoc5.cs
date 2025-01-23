using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLoc5 : MonoBehaviour
{
    private bool isColliding;
    [SerializeField] GameObject DialogueBox;
    [SerializeField] DialogueLoc5 dialogue;
    [SerializeField] Movement movement;
    [SerializeField] DetailManager dm;
    private bool isPlayerInZone = false;
    [SerializeField] public static bool dialogueStarted = false;

    private void Start()
    {
        DialogueBox.SetActive(false);
    }

    private void Update()
    {
        isColliding = false;
        if (isPlayerInZone && (dm.detailCount == 5) && !dialogueStarted)
        {
            DialogueBox.SetActive(true);
            dialogueStarted = true;
            dialogue.StartDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isColliding) return;
            isColliding = true;
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }
}
