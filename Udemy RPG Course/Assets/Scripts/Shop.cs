using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    [SerializeField] GameObject shopMenu = null;
    [SerializeField] GameObject buyMenu = null;
    [SerializeField] GameObject sellMenu = null;
    [SerializeField] Text moneyText = null;
    public string[] itemsForSale { get; set; }
    public GameObject BuyMenu { get => buyMenu; }
    public GameObject SellMenu { get => sellMenu; }
    public GameObject ShopMenu { get => shopMenu; }

    [SerializeField] ItemButton[] buyItemButtons = null;
    [SerializeField] ItemButton[] sellItemButtons = null;
    [SerializeField] Text buyItemName = null;
    [SerializeField] Text buyItemDescription = null;
    [SerializeField] Text buyItemValue = null;
    [SerializeField] Text sellItemName = null;
    [SerializeField] Text sellItemDescription = null;
    [SerializeField] Text sellItemValue = null;
    Item selectedItem = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OpenShop()
    {
        if (PlayerController.instance.canMove)
        {
            PlayerController.instance.canMove = false;
            UpdateMoney();
            shopMenu.SetActive(true);
            OpenBuyMenu();
        }
    }

    public void CloseShop()
    {
        PlayerController.instance.canMove = true;
        shopMenu.SetActive(false);
    }

    public void OpenBuyMenu()
    {
        selectedItem = null;
        buyItemName.text = "";
        buyItemDescription.text = "";
        buyItemValue.text = "";
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].ItemImage.gameObject.SetActive(false);
            buyItemButtons[i].AmountText.text = "";
            buyItemButtons[i].Index = -1;
        }
        for (int i = 0; i < itemsForSale.Length; i++)
        {
            buyItemButtons[i].Index = i;
            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].ItemImage.gameObject.SetActive(true);
                buyItemButtons[i].ItemImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).ItemSprite;
            }
            else
            {
                buyItemButtons[i].ItemImage.gameObject.SetActive(false);
            }
        }
    }

    public void OpenSellMenu()
    {
        selectedItem = null;
        sellItemName.text = "";
        sellItemDescription.text = "";
        sellItemValue.text = "";
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);
        ShowSellItems();
    }

    private void ShowSellItems()
    {
        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].Index = i;
            if (GameManager.instance.ItemsHeld[i] != "")
            {
                sellItemButtons[i].ItemImage.gameObject.SetActive(true);
                sellItemButtons[i].ItemImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld[i]).ItemSprite;
                sellItemButtons[i].AmountText.text = GameManager.instance.NumberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].ItemImage.gameObject.SetActive(false);
                sellItemButtons[i].AmountText.text = "";
            }
        }
    }

    public void UpdateMoney()
    {
        moneyText.text = "Gold:\n" + GameManager.instance.CurrentGold;
    }

    public void SelectBuyItem(Item buyItem)
    {
        if (buyItem != null)
        {
            selectedItem = buyItem;
            buyItemName.text = selectedItem.ItemName;
            buyItemDescription.text = selectedItem.Description;
            buyItemValue.text = "Value: " + selectedItem.Value;
        }
    }

    public void SelectSellItem(Item sellItem)
    {
        if (sellItem != null)
        {
            selectedItem = sellItem;
            sellItemName.text = selectedItem.ItemName;
            sellItemDescription.text = selectedItem.Description;
            sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.Value * .5f).ToString();
        }
    }

    public void BuyItem()
    {
        if (selectedItem != null)
        {
            if (GameManager.instance.CurrentGold >= selectedItem.Value)
            {
                GameManager.instance.SpendGold(selectedItem.Value);
                GameManager.instance.AddItem(selectedItem.ItemName);
            }
            moneyText.text = "Gold:\n" + GameManager.instance.CurrentGold;
        }
    }

    public void SellItem()
    {
        if (selectedItem != null && GameManager.instance.FindItem(selectedItem.ItemName) != -1)
        {
            GameManager.instance.AddGold(Mathf.FloorToInt(selectedItem.Value * .5f));
            GameManager.instance.RemoveItem(selectedItem.ItemName);
        }
        moneyText.text = "Gold:\n" + GameManager.instance.CurrentGold;
        ShowSellItems();
    }
}
