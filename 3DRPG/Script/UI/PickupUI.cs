using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupUI : MonoBehaviour
{
    public static PickupUI instance;


    public TextMeshProUGUI text;


    private void Awake()
    {
        instance = this;

        Hide();
    }



    public void Show(string itemName)
    {

        text.text = "[E] Pick Up " + itemName;

        gameObject.SetActive(true);

    }



    public void Hide()
    {

        gameObject.SetActive(false);

    }
}
