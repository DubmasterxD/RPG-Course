using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemActivator : MonoBehaviour
{
    [SerializeField] GameObject objectToActivate = null;
    [SerializeField] string questToCheck ="";
    [SerializeField] bool activeIfComplete=false;

    bool initialCheckDone = false;

    private void Update()
    {
        if(!initialCheckDone)
        {
            initialCheckDone = true;
            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        if(QuestManager.instance.CheckIfComplete(questToCheck))
        {
            objectToActivate.SetActive(activeIfComplete);
        }
    }
}
