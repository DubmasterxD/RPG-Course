using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] string sceneToLoad = "";
    [SerializeField] float waitToLoad = 1f;

    private void Start()
    {
        GetComponentInChildren<AreaEntrance>().SetPreviousScene(sceneToLoad);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.instance.lastScene = SceneManager.GetActiveScene().name;
            UIFade.instance.TriggerFadeIn();
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
