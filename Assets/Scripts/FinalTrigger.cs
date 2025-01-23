using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    [SerializeField] GameObject DialogueBox;
    [SerializeField] FinalDialogue dialogue;
    [SerializeField] GameObject exit;

    private void Start()
    {
        DialogueBox.SetActive(false);
        StartCoroutine(TextAfterDelay(6.5f));
        StartCoroutine(ExitAfterDelay(12f));
    }

    IEnumerator TextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DialogueBox.SetActive(true);
        dialogue.StartDialogue();
    }
    IEnumerator ExitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        exit.SetActive(true);
    }
}

