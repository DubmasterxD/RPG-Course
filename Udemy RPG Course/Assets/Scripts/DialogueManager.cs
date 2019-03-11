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
    string questToMark;
    bool markQuestComplete;
    bool shouldMarkQuest;

    public GameObject DialogueBox { get => dialogueBox; }

    private void Update()
    {
        if(Input.GetButtonUp("Fire1") && dialogueBox.activeInHierarchy)
        {
            currLine++;
            UpdateDialogueText();
        }
    }

    public void ShowDialogue(string[] newLines, string NPCName, bool isSign)
    {
        if (PlayerController.instance.canMove)
        {
            PlayerController.instance.canMove = false;
            if (isSign)
            {
                nameBox.SetActive(false);
            }
            else
            {
                nameBox.SetActive(true);
                nameText.text = NPCName;
            }
            dialogueLines = newLines;
            currLine = 0;
            dialogueBox.SetActive(true);
            UpdateDialogueText();
            currLine = -1;
        }
    }

    void UpdateDialogueText()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (currLine >= dialogueLines.Length)
            {
                dialogueBox.SetActive(false);
                PlayerController.instance.canMove = true;
                if(shouldMarkQuest)
                {
                    shouldMarkQuest = false;
                    if(markQuestComplete)
                    {
                        QuestManager.instance.MarkQuestComplete(questToMark);
                    }
                    else
                    {
                        QuestManager.instance.MarkQuestIncomplete(questToMark);
                    }
                }
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
    }

    public void ShouldActivateQuestAtEnd(string questName, bool markComplete)
    {
        questToMark = questName;
        markQuestComplete = markComplete;
        shouldMarkQuest = true;
    }
}
