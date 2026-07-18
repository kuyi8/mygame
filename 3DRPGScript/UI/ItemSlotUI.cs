using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private TMP_Text itemCount;

    public void SetItem(ItemData item)
    {
        // 当前还没有物品堆叠系统
        itemCount.text = "×1";

        // 从 ItemData 中读取物品图标
        if (item.icon != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else
        {
            // 物品没有设置图标时，隐藏图标容器
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}