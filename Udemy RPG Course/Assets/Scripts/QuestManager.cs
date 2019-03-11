using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] string[] questMarkerNames = null;
    [SerializeField] bool[] questMarkerCompletion = null;

    public static QuestManager instance;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        questMarkerCompletion = new bool[questMarkerNames.Length];
    }

    public bool CheckIfComplete(string questName)
    {
        int questNumber = GetQuestNumber(questName);
        if(questNumber!=-1)
        {
            return questMarkerCompletion[questNumber];
        }
        return false;
    }

    public void MarkQuestComplete(string questName)
    {
        int questNumber = GetQuestNumber(questName);
        if (questNumber != -1)
        {
            questMarkerCompletion[questNumber] = true;
        }
        UpdateLocalQuestObjects();
    }

    public void MarkQuestIncomplete(string questName)
    {
        int questNumber = GetQuestNumber(questName);
        if (questNumber != -1)
        {
            questMarkerCompletion[questNumber] = false;
        }
        UpdateLocalQuestObjects();
    }

    private int GetQuestNumber(string questName)
    {
        for(int i=0; i<questMarkerNames.Length;i++)
        {
            if(questMarkerNames[i]==questName)
            {
                return i;
            }
        }
        Debug.LogError("Quest " + questName + " does not exist");
        return -1;
    }

    public void UpdateLocalQuestObjects()
    {
        QuestItemActivator[] questObjects = FindObjectsOfType<QuestItemActivator>();
        for(int i=0; i<questObjects.Length;i++)
        {
            questObjects[i].CheckCompletion();
        }
    }

    public void SaveQuestData()
    {
        for(int i=0; i<questMarkerNames.Length;i++)
        {
            if(questMarkerCompletion[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            if (PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i]))
            {
                int result = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
                if (result == 0)
                {
                    questMarkerCompletion[i] = false;
                }
                else
                {
                    questMarkerCompletion[i] = true;
                }
            }
        }
    }
}
