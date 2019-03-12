using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [SerializeField] bool battleActive = false;
    [SerializeField] GameObject battleSceme = null;
    [SerializeField] Transform[] playerPositions = null;
    [SerializeField] Transform[] enemyPositions = null;
    [SerializeField] BattleChar[] playerPrefabs = null;
    [SerializeField] BattleChar[] enemyPrefabs = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BattleStart(string[] enemiesToSpawn)
    {
        if(!battleActive)
        {
            battleActive = true;
            PlayerController.instance.canMove = false;
            battleSceme.SetActive(true);
        }
    }
}
