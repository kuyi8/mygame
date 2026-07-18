using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardControl : MonoBehaviour
{
    [HideInInspector]
    public string cardName;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Load(string name)
    { 
        cardName = name;
        image.sprite = Resources.Load<Sprite>(name);
    }
}
