using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] CharStats[] playerStats = null;

    [SerializeField] string[] itemsHeld=null;
    [SerializeField] int[] numberOfItems = null;
    [SerializeField] Item[] referenceItems = null;

    public CharStats[] PlayerStats { get => playerStats; }
    public string[] ItemsHeld { get => itemsHeld;}
    public int[] NumberOfItems { get => numberOfItems; }

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
        for(int i=0; i<itemsHeld.Length;i++)
        {
            for(int j=i+1;j<itemsHeld.Length;j++)
            {
                if (Greater(itemsHeld[j], itemsHeld[i]))
                {
                    Swap(j, i);
                }
                if(itemsHeld[j]=="")
                {
                    break;
                }
            }
            if(itemsHeld[i]=="")
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
                    if(itemsHeld[i]=="")
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
                Debug.LogError("Item "+itemToAdd+" doesn't exist");
            }
        }
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;
        for (int i = 0; i< itemsHeld.Length;i++)
        {
            if(itemsHeld[i]==itemToRemove)
            {
                foundItem = true;
                itemPosition = i;
                break;  
            }
        }
        if(foundItem)
        {
            numberOfItems[itemPosition]--;
            if(numberOfItems[itemPosition]<=0)
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
}
