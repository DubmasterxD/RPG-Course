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
    public string[] itemsForSale { get;  set; }
    [SerializeField] ItemButton[] buyItemButtons = null;
    [SerializeField] ItemButton[] sellItemButtons = null;

    private void Awake()
    {
        if(instance==null)
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
        buyMenu.SetActive(true);
        sellMenu.SetActive(false);
        for(int i=0; i<buyItemButtons.Length;i++)
        {
            buyItemButtons[i].ItemImage.gameObject.SetActive(false);
            buyItemButtons[i].AmountText.text = "";
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
        buyMenu.SetActive(false);
        sellMenu.SetActive(true);
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
}
