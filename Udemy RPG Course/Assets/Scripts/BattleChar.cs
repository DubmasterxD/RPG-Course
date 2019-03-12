using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    [SerializeField] bool isPlayer = false;
    [SerializeField] string[] movesAvailable = null;

    [SerializeField] string charName = "";
    [SerializeField] int currentHP = 100;
    [SerializeField] int maxHP = 100;
    [SerializeField] int currentMP = 30;
    [SerializeField] int MaxMP = 30;
    [SerializeField] int strength = 10;
    [SerializeField] int defence = 10;
    [SerializeField] int wpnPower = 10;
    [SerializeField] int armorPower = 10;
    [SerializeField] bool hasDied = false;

}
