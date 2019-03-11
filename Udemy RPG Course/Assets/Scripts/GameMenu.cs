using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject theMenu = null;
    [SerializeField] GameObject[] windows = null;
    [SerializeField] GameObject[] statusButtons = null;
    [SerializeField] Text[] nameText = null;
    [SerializeField] Text[] hpText = null;
    [SerializeField] Text[] mpText = null;
    [SerializeField] Text[] levelText = null;
    [SerializeField] Text[] expText = null;
    [SerializeField] Slider[] expSlider = null;
    [SerializeField] Text[] expPercentage = null;
    [SerializeField] Image[] charImage = null;
    [SerializeField] GameObject[] charStatsHolder = null;
    [SerializeField] Text statusName = null;
    [SerializeField] Text statusHP = null;
    [SerializeField] Text statusMP = null;
    [SerializeField] Text statusStr = null;
    [SerializeField] Text statusDef = null;
    [SerializeField] Text statusWpnEq = null;
    [SerializeField] Text statusWpnPower = null;
    [SerializeField] Text statusArmorEq = null;
    [SerializeField] Text statusArmorPower = null;
    [SerializeField] Text statusEXP = null;
    [SerializeField] Text statusEXPPerecentage = null;
    [SerializeField] Slider statusEXPSlider = null;
    [SerializeField] Image statusImage = null;
    [SerializeField] ItemButton[] itemButtons = null;
    [SerializeField] Item activeItem = null;
    [SerializeField] Text itemName = null;
    [SerializeField] Text itemDescription = null;
    [SerializeField] Text useButtonText = null;
    [SerializeField] GameObject itemCharChoiceMenu = null;
    [SerializeField] Text[] itemCharChoiceNames = null;
    [SerializeField] Text goldText = null;
    [SerializeField] string mainMenuName = "";

    CharStats[] playerStats = null;
    int charsInfoIndex = 0;

    public static GameMenu instance;

    public GameObject TheMenu { get => theMenu; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

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
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                charStatsHolder[i].SetActive(true);
                nameText[i].text = playerStats[i].CharName;
                hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].MaxHP;
                mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].MaxMP;
                levelText[i].text = "Level: " + playerStats[i].PlayerLevel;
                expText[i].text = "EXP: " + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].PlayerLevel];
                expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].PlayerLevel];
                expSlider[i].value = playerStats[i].currentEXP;
                expPercentage[i].text = "" + Mathf.FloorToInt(expSlider[i].normalizedValue * 100) + "%";
                charImage[i].sprite = playerStats[i].CharImage;
            }
            else
            {
                charStatsHolder[i].SetActive(false);
            }
        }
        goldText.text = "Gold:\n" + GameManager.instance.CurrentGold;
    }

    public void ToggleWindow(int windowNumber)
    {
        UpdatePlayerStats();
        if (!windows[windowNumber].activeInHierarchy)
        {
            for (int i = 0; i < windows.Length; i++)
            {
                if (windows[i].name == "Chars info")
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
        itemCharChoiceMenu.SetActive(false);
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
            if (windows[i].name == "Chars info")
            {
                windows[i].SetActive(true);
            }
        }
        theMenu.SetActive(false);
        PlayerController.instance.canMove = true;
        itemCharChoiceMenu.SetActive(false);
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

    public void ShowItems()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].Index = i;
            if (GameManager.instance.ItemsHeld[i] != "")
            {
                itemButtons[i].ItemImage.gameObject.SetActive(true);
                itemButtons[i].ItemImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld[i]).ItemSprite;
                itemButtons[i].AmountText.text = GameManager.instance.NumberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].ItemImage.gameObject.SetActive(false);
                itemButtons[i].AmountText.text = "";
            }
        }
    }

    public void SortItems()
    {
        GameManager.instance.SortItems();
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;
        if (activeItem.IsItem)
        {
            useButtonText.text = "Use";
        }
        else if (activeItem.IsArmour || activeItem.IsWeapon)
        {
            useButtonText.text = "Equip";
        }
        itemName.text = activeItem.ItemName;
        itemDescription.text = activeItem.Description;
    }

    public void DiscardItem()
    {
        if (activeItem != null && GameManager.instance.FindItem(activeItem.ItemName) != -1)
        {
            GameManager.instance.RemoveItem(activeItem.ItemName);
        }
    }

    public void OpenItemCharacterChoice()
    {
        if (activeItem!=null &&GameManager.instance.FindItem(activeItem.ItemName) != -1)
        {
            itemCharChoiceMenu.SetActive(true);
            for (int i = 0; i < itemCharChoiceNames.Length; i++)
            {
                itemCharChoiceNames[i].text = GameManager.instance.PlayerStats[i].CharName;
                itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.PlayerStats[i].gameObject.activeInHierarchy);
            }
        }
    }

    public void CloseItemCharacterChoice()
    {
        itemCharChoiceMenu.SetActive(false);
    }

    public void UseItem(int selectChar)
    {
        activeItem.Use(selectChar);
        CloseItemCharacterChoice();
    }

    public void SaveGame()
    {
        GameManager.instance.SaveData();
        QuestManager.instance.SaveQuestData();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(mainMenuName);
        Destroy(GameManager.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(AudioManager.instance.gameObject);
        Destroy(gameObject);
    }
}