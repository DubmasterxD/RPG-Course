using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    Transform target;

    [SerializeField] int musicToPlay = 0;
    private bool musicStarted = false;
        
    private void Start()
    {
        target = PlayerController.instance.transform;
    }
    
    private void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            if (!musicStarted)
            {
                musicStarted = true;
                AudioManager.instance.PlayBGM(musicToPlay);
            }
        }
    }
}
