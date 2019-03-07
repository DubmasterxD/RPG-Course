using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpen = false;
    [SerializeField] string[] itemsForSell = null;
    [SerializeField] GameObject shopMenu = null;

    private void Update()
    {
        if (canOpen && Input.GetButtonDown("Fire1") && !shopMenu.activeInHierarchy)
        {
            Shop.instance.itemsForSale = itemsForSell;
            Shop.instance.OpenShop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canOpen = false;
        }
    }
}
