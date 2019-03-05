using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] string previousScene = "";

    public string PreviousScene {set => previousScene = value; }

    private void Start()
    {
        if(previousScene==PlayerController.instance.lastScene)
        {
            PlayerController.instance.transform.position = transform.position;
        }
        UIFade.instance.TriggerFadeOut();
    }
}
