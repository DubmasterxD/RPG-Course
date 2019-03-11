using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string loadGameScene = "";
    [SerializeField] string newGameScene = "";
    [SerializeField] GameObject contButton = null;

    private void Start()
    {
        if(PlayerPrefs.HasKey("Current_Scene"))
        {
            contButton.SetActive(true);
        }
        else
        {
            contButton.SetActive(false);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(loadGameScene);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
