using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Text amountText;
    [SerializeField] int index;

    public int Index { get => index; set => index = value; }
    public Image ItemImage { get => itemImage; set => itemImage = value; }
    public Text AmountText { get => amountText; set => amountText = value; }

    public void Press()
    {
        if (GameMenu.instance.TheMenu.activeInHierarchy)
        {
            if (GameManager.instance.ItemsHeld[index] != "")
            {
                GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld[index]));
            }
        }
        if (Shop.instance.ShopMenu.activeInHierarchy)
        {
            if (Shop.instance.BuyMenu.activeInHierarchy && index != -1)
            {
                Shop.instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.instance.itemsForSale[index]));

            }
            if (Shop.instance.SellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld[index]));
            }
        }
    }
}
