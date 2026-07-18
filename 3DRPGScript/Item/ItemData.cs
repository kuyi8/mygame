using UnityEngine;


public enum ItemType
{
    Weapon,      //武器
    Armor,       //防具
    Consumable,  //消耗品
    Material,    //材料
    QuestItem    //任务物品
}



public enum ItemQuality
{
    Common,      //普通
    Uncommon,    //优秀
    Rare,        //稀有
    Epic,        //史诗
    Legendary    //传说
}



[CreateAssetMenu(menuName = "RPG/Item")]
public class ItemData : ScriptableObject
{

    //物品编号
    public int id;


    //物品名称
    public string itemName;


    //物品图标
    public Sprite icon;


    //物品类型
    public ItemType itemType;


    //物品品质
    public ItemQuality quality;


    //描述
    [TextArea]
    public string description;

}