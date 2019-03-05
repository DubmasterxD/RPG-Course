using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject theMenu = null;
    [SerializeField] GameObject[] windows = null;
    [SerializeField] GameObject[] statusButtons = null;
    [SerializeField] Text[] nameText, hpText, mpText, levelText, expText;
    [SerializeField] Slider[] expSlider = null;
    [SerializeField] Text[] expPercentage = null;
    [SerializeField] Image[] charImage = null;
    [SerializeField] GameObject[] charStatsHolder = null;
    [SerializeField] Text statusName, statusHP, statusMP, statusStr, statusDef, statusWpnEq, statusWpnPower, statusArmorEq, statusArmorPower, statusEXP, statusEXPPerecentage;
    [SerializeField] Slider statusEXPSlider = null;
    [SerializeField] Image statusImage = null;

    CharStats[] playerStats = null;
    int charsInfoIndex = 0;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (theMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void UpdatePlayerStats()
    {
        playerStats = GameManager.instance.PlayerStats;
        for(int i=0; i<playerStats.Length; i++)
        {
            if(playerStats[i].gameObject.activeInHierarchy)
            {
                charStatsHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].CharName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].MaxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].MaxMP;
                levelText[i].text = "Level: " + playerStats[i].PlayerLevel;
                expText[i].text = "EXP: " + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].PlayerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].PlayerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                expPercentage[i].text = "" + Mathf.FloorToInt(expSlider[i].normalizedValue * 100) +"%";
                charImage[i].sprite = playerStats[i].CharImage;
            }
            else
            {
                charStatsHolder[i].SetActive(false);
            }
        }
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdatePlayerStats();
        if (!windows[windowNumber].activeInHierarchy)
        {
            for (int i = 0; i < windows.Length; i++)
            {
                if(windows[i].name=="Chars info")
                {
                    charsInfoIndex = i;
                }
                windows[i].SetActive(false);
            }
            windows[windowNumber].SetActive(true);
        }
        else
        {
            windows[windowNumber].SetActive(false);
            windows[charsInfoIndex].SetActive(true);
        }
    }

    public void OpenStatsMenu()
    {
        ToggleWindow(1);
        StatusChar(0);
        for (int i = 0; i < statusButtons.Length; i++)
        {
            statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
            statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].CharName;
        }
    }

    public void StatusChar(int selected)
    {
        statusName.text = playerStats[selected].CharName;
        statusHP.text = playerStats[selected].currentHP.ToString() + "/" + playerStats[selected].MaxHP;
        statusMP.text = playerStats[selected].currentMP.ToString() + "/" + playerStats[selected].MaxMP;
        statusStr.text = playerStats[selected].Strength.ToString();
        statusDef.text = playerStats[selected].Defence.ToString();
        statusWpnEq.text = playerStats[selected].EquippedWeapon;
        statusWpnPower.text = playerStats[selected].WeaponPower.ToString();
        statusArmorEq.text = playerStats[selected].EquippedArmor;
        statusArmorPower.text = playerStats[selected].ArmorPower.ToString();
        statusEXP.text = playerStats[selected].currentEXP.ToString() + "/" + playerStats[selected].expToNextLevel[playerStats[selected].PlayerLevel];
        statusEXPSlider.maxValue = playerStats[selected].expToNextLevel[playerStats[selected].PlayerLevel];
        statusEXPSlider.value = playerStats[selected].currentEXP;
        statusEXPPerecentage.text = Mathf.FloorToInt(statusEXPSlider.normalizedValue * 100).ToString();
        statusImage.sprite = playerStats[selected].CharImage;
    }

    public void CloseMenu()
    {
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
            if(windows[i].name=="Chars info")
            {
                windows[i].SetActive(true);
            }
        }
        theMenu.SetActive(false);
        PlayerController.instance.canMove = true;
    }

    public void OpenMenu()
    {
        if (PlayerController.instance.canMove)
        {
            UpdatePlayerStats();
            theMenu.SetActive(true);
            PlayerController.instance.canMove = false;
        }
    }
}

