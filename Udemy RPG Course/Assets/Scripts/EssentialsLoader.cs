using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject UICanvas = null;
    [SerializeField] GameObject gameMan = null;
    [SerializeField] GameObject audioManager = null;

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
        if (GameManager.instance == null)
        {
            Instantiate(gameMan);
        }
        if (AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }
    }
}
