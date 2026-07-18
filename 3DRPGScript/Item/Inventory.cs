using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public int capacity = 20;

    //保存玩家拥有的物品
    public List<ItemData> items = new List<ItemData>();

    public bool AddItem(ItemData item)
    {
        if (items.Count >= capacity)
        {
            Debug.Log("背包已满");
            return false;
        }
        items.Add(item);


        Debug.Log("获得物品：" + item.itemName);
        return true;
    }

    public List<ItemData> GetItems()
    {
        return items;
    }

}