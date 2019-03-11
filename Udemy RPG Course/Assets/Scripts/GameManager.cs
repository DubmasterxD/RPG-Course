using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] CharStats[] playerStats = null;

    [SerializeField] string[] itemsHeld = null;
    [SerializeField] int[] numberOfItems = null;
    [SerializeField] Item[] referenceItems = null;

    public CharStats[] PlayerStats { get => playerStats; }
    public string[] ItemsHeld { get => itemsHeld; }
    public int[] NumberOfItems { get => numberOfItems; }
    public int CurrentGold { get => currentGold; }

    [SerializeField] int currentGold = 0;

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

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].ItemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }
        return null;
    }

    public void SortItems()
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "")
            {
                for (int j = i + 1; j < itemsHeld.Length; j++)
                {
                    if (itemsHeld[j] != "")
                    {
                        Swap(i, j);
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            for (int j = i + 1; j < itemsHeld.Length; j++)
            {
                if (Greater(itemsHeld[j], itemsHeld[i]))
                {
                    Swap(j, i);
                }
                if (itemsHeld[j] == "")
                {
                    break;
                }
            }
            if (itemsHeld[i] == "")
            {
                break;
            }
        }
    }

    private bool Greater(string item1, string item2)
    {
        if (item1 == item2)
        {
            return false;
        }
        int item1index = referenceItems.Length, item2index = referenceItems.Length;
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].ItemName == item1)
            {
                item1index = i;
            }
            if (referenceItems[i].ItemName == item2)
            {
                item2index = i;
            }
        }
        if (item1index < item2index)
        {
            return true;
        }
        return false;
    }

    private void Swap(int item1, int item2)
    {
        string tmp = itemsHeld[item1];
        itemsHeld[item1] = itemsHeld[item2];
        itemsHeld[item2] = tmp;
        int tmp2 = numberOfItems[item1];
        numberOfItems[item1] = numberOfItems[item2];
        numberOfItems[item2] = tmp2;
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundItem = false;
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                foundItem = true;
                break;
            }
        }
        if (foundItem)
        {
            numberOfItems[newItemPosition]++;
        }
        else
        {
            bool itemExists = false;
            for (int i = 0; i < referenceItems.Length; i++)
            {
                if (referenceItems[i].ItemName == itemToAdd)
                {
                    itemExists = true;
                    break;
                }
            }
            if (itemExists)
            {
                for (int i = 0; i < itemsHeld.Length; i++)
                {
                    if (itemsHeld[i] == "")
                    {
                        newItemPosition = i;
                        break;
                    }
                }
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition] = 1;
            }
            else
            {
                Debug.LogError("Item " + itemToAdd + " doesn't exist");
            }
        }
    }

    public void RemoveItem(string itemToRemove)
    {
        int itemPosition = FindItem(itemToRemove);
        if (itemPosition != -1)
        {
            numberOfItems[itemPosition]--;
            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }
            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Couldn't find " + itemToRemove);
        }
    }

    public int FindItem(string itemToFind)
    {
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToFind)
            {
                return i;
            }
        }
        return -1;
    }

    public void SpendGold(int goldToSpend)
    {
        currentGold -= goldToSpend;
    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerController.instance.transform.position.z);

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_active", 0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_Level", playerStats[i].PlayerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_CurrentExp", playerStats[i].currentEXP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_CurrentHP", playerStats[i].currentHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_MaxHP", playerStats[i].MaxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_CurrentMP", playerStats[i].currentMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_MaxMP", playerStats[i].MaxMP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_Strength", playerStats[i].Strength);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_Defence", playerStats[i].Defence);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_WpnPwr", playerStats[i].WeaponPower);
            PlayerPrefs.SetInt("Player_" + playerStats[i].CharName + "_ArmrPwr", playerStats[i].ArmorPower);
            PlayerPrefs.SetString("Player_" + playerStats[i].CharName + "_EqWpn", playerStats[i].EquippedWeapon);
            PlayerPrefs.SetString("Player_" + playerStats[i].CharName + "_EqArmr", playerStats[i].EquippedArmor);
        }

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }
    }

    public void LoadData()
    {
        PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);
            }
            playerStats[i].PlayerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_Level");
            playerStats[i].currentEXP = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_CurrentExp");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_CurrentHP");
            playerStats[i].MaxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_MaxHP");
            playerStats[i].currentMP = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_CurrentMP");
            playerStats[i].MaxMP = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_MaxMP");
            playerStats[i].Strength = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_Strength");
            playerStats[i].Defence = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_Defence");
            playerStats[i].WeaponPower = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_WpnPwr");
            playerStats[i].WeaponPower = PlayerPrefs.GetInt("Player_" + playerStats[i].CharName + "_ArmrPwr");
            playerStats[i].EquippedWeapon = PlayerPrefs.GetString("Player_" + playerStats[i].CharName + "_EqWpn");
            playerStats[i].EquippedArmor = PlayerPrefs.GetString("Player_" + playerStats[i].CharName + "_EqArmr");
        }

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }
}
