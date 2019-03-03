using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public static UIFade instance;

    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] Image fadeScreen;
    bool fadeIn;
    bool fadeOut;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        
        if(fadeIn)
        {
            PlayerController.instance.canMove = false;
            PlayerController.instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            fadeScreen.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a==1f)
            {
                fadeIn = false;
            }
        }
        else if(fadeOut)
        {
            fadeScreen.color = new Color(0f, 0f, 0f, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOut = false;
                PlayerController.instance.canMove = true;
            }
        }
    }

    public void TriggerFadeIn()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void TriggerFadeOut()
    {
        fadeOut = true;
        fadeIn = false;
    }
}
