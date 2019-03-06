using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    bool canPickup = false;

    private void Update()
    {
        if(canPickup && Input.GetButtonDown("Fire1")&& PlayerController.instance.canMove)
        {
            GameManager.instance.AddItem(GetComponent<Item>().ItemName);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canPickup = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canPickup = false;
    }
}
