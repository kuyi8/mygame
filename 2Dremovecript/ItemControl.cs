using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Collider2D itemCollider;

    [HideInInspector]
    public string imageName;
    public Action<ItemControl> handler;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        itemCollider = GetComponent<Collider2D>();
    }

    public void LoadImage(string name , int layer = 0)
    { 
        imageName = name;
        sprite.sprite = Resources.Load<Sprite>(name);
        sprite.sortingOrder = layer;
    }

    public void Check()
    {
        bool isBlocked = false;

        Collider2D[] res = Physics2D.OverlapBoxAll(
            transform.position,
            new Vector2(1.28f, 1.28f),
            0
        );

        foreach (var col in res)
        {
            if (col == itemCollider) continue; // Ìø¹ý×Ô¼º

            var otherSR = col.GetComponent<SpriteRenderer>();
            if (otherSR == null) continue;

            if (otherSR.sortingOrder > sprite.sortingOrder)
            {
                isBlocked = true;
                break;
            }
        }

        sprite.color = isBlocked ? Color.black : Color.white;
    }

    //private void OnMouseDown()
    //{
    //    if(handler != null)
    //        handler(this);
    //}
    public void OnClick()
    {
        if (GetComponent<SpriteRenderer>().color == Color.black)
            return;

        handler?.Invoke(this);
    }
}
