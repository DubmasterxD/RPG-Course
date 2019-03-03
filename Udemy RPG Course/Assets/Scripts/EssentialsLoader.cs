using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject UICanvas;

    private void Start()
    {
        if (UIFade.instance == null)
        {
            Instantiate(UICanvas);
        }
        if (PlayerController.instance == null)
        {
            Instantiate(player);
        }
    }
}
