using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivation : MonoBehaviour
{
    [SerializeField] string[] lines = null;
    [SerializeField] bool isSign = false;
    bool canActivate = false;

    DialogueManager dialogueManager = null;

    private void Start()
    {
        dialogueManager = UIFade.instance.gameObject.GetComponent<DialogueManager>();
    }

    private void Update()
    {
        bool isActivated = dialogueManager.GetDialogueBox().activeInHierarchy;
        if (canActivate && Input.GetButtonDown("Fire1") && !isActivated)
        {
            dialogueManager.ShowDialogue(lines, gameObject.name, isSign);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
