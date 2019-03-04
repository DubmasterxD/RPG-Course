using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Text dialogueText = null;
    [SerializeField] Text nameText = null;
    [SerializeField] GameObject dialogueBox = null;
    [SerializeField] GameObject nameBox = null;

    string[] dialogueLines = null;
    int currLine = 0;

    private void Update()
    {
        if(Input.GetButtonUp("Fire1"))
        {
            currLine++;
            UpdateDialogueText();
        }
    }

    public void ShowDialogue(string[] newLines, string NPCName, bool isSign)
    {
        PlayerController.instance.canMove = false;
        if(isSign)
        {
            nameBox.SetActive(false);
        }
        else
        {
            nameBox.SetActive(true);
            nameText.text = NPCName;
        }
        dialogueLines = newLines;
        currLine = -1;
        UpdateDialogueText();
        dialogueBox.SetActive(true);
    }

    void UpdateDialogueText()
    {
        if (currLine >= dialogueLines.Length)
        {
            dialogueBox.SetActive(false);
            PlayerController.instance.canMove = true;
        }
        else if (currLine >= 0)
        {
            dialogueText.text = dialogueLines[currLine];
        }
        else
        {
            dialogueText.text = "";
        }
    }

    public GameObject GetDialogueBox()
    {
        return dialogueBox;
    }
}
