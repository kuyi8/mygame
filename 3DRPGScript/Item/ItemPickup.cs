using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData item;
    private bool playerInRange = false;


    private Inventory playerInventory;



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("进入拾取范围");

        //Debug.Log("进入触发范围");
        if (other.CompareTag("Player"))
        {
            Debug.Log("识别到玩家");
            Inventory inventory =
     other.GetComponentInParent<Inventory>();


            if (inventory != null)
            {

                playerInRange = true;

                playerInventory = inventory;


                PickupUI.instance.Show(item.itemName);

            }

        }

    }



    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            Inventory inventory = other.GetComponentInParent<Inventory>();


            if (inventory != null)
            {

                playerInRange = false;

                playerInventory = inventory;


                PickupUI.instance.Hide();

            }

        }
    }



    private void Update()
    {

        if (playerInRange)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                PickUp();

            }

        }

    }



    void PickUp()
    {

        bool success = playerInventory.AddItem(item);


        if (success)
        {
            Destroy(gameObject);
            PickupUI.instance.Hide();
        }

    }
}
