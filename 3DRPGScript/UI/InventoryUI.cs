using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private ItemSlotUI itemSlotPrefab;

    [SerializeField]
    private Inventory playerInventory;


    private void Awake()
    {
        instance = this;

        // 游戏开始时关闭背包面板
        panel.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (panel.activeSelf)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
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
        Debug.Log("开始刷新背包，当前物品数量：" + playerInventory.items.Count);

        // 删除上一次生成的物品格子
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // 为背包中的每件物品生成一个格子
        foreach (ItemData item in playerInventory.items)
        {
            Debug.Log("生成物品格子：" + item.itemName);

            ItemSlotUI newSlot =
                Instantiate(itemSlotPrefab, content);

            newSlot.SetItem(item);
        }
    }
}