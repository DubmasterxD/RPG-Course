using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject UICanvas = null;

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
