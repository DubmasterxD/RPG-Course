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
        if(GameManager.instance.ItemsHeld[index]!="")
        {
            GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld[index]));
        }
    }
}
