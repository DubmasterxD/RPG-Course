using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour
{
    [SerializeField] string questToMark ="";
    [SerializeField] bool markComplete = false;
    [SerializeField] bool markOnEnter = true;
    [SerializeField] bool canMark = false;
    [SerializeField] bool deactivateOnMarking = true;

    private void Update()
    {
        if(canMark && Input.GetButtonDown("Fire1"))
        {
            canMark = false;
            MarkQuest();
        }
    }

    public void MarkQuest()
    {
        canMark = false;
        if(markComplete)
        {
            QuestManager.instance.MarkQuestComplete(questToMark);
        }
        else
        {
            QuestManager.instance.MarkQuestIncomplete(questToMark);
        }
        gameObject.SetActive(!deactivateOnMarking);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (markOnEnter)
            {
                MarkQuest();
            }
            else
            {
                canMark = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canMark = false;
        }
    }
}
