using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] sfx = null;
    [SerializeField] AudioSource[] bgm = null;

    public static AudioManager instance;

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

    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < sfx.Length)
        {
            sfx[soundToPlay].Play();
        }
    }

    public void PlayBGM(int musicToPlay)
    {
        if (!bgm[musicToPlay].isPlaying)
        {
            StopMusic();
            if (musicToPlay < bgm.Length)
            {
                bgm[musicToPlay].Play();

            }
        }
    }

    public void StopMusic()
    {
        for(int i =0; i<bgm.Length;i++)
        {
            bgm[i].Stop();
        }
    }
}
