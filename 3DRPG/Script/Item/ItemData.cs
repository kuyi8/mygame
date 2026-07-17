using UnityEngine;

public enum ItemType
{
    Weapon,
    Armor,
    Consumable,
    Material,
    QuestItem
}

public enum ItemQuality
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "RPG/Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;
    public ItemQuality quality;

    [TextArea]
    public string description;
}
