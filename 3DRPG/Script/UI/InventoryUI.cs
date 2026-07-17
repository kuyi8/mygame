using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;


    public GameObject panel;


    public TextMeshProUGUI itemText;



    private Inventory playerInventory;



    private void Awake()
    {
        instance = this;
    }



    public void Open()
    {

        panel.SetActive(true);

        Refresh();

    }



    public void Close()
    {

        panel.SetActive(false);

    }



    public void Refresh()
    {

        itemText.text = "";


        foreach (ItemData item in playerInventory.items)
        {

            itemText.text +=
            item.itemName + "\n";

        }

    }
}
