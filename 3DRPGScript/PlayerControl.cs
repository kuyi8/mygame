using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //单例
    public static PlayerControl Instance;
    //人物最大血量
    public int MaxHp = 10;
    //人物当前血量
    public int hp = 10;
    //人物当前血量属性
   public int Hp
   {
       set
       {
           hp = value;
           //做一个简单的数值限制
           //如果血量大于10
           if (hp > 10)
           {
               //血量设置为10
               hp = 10;
           }
           //如果血量小于0
           if (hp < 0)
           {
               hp = 0;
           }
           //设置血量显示
           PlayerPanelControl.Instance.SetHp(hp);
       }
       get
       {
           return hp;
       }
   }
    //攻击力
    public int Attack = 3;

    void Awake()
    {
        //单例
        Instance = this;
    }

    //玩家受到伤害
    public void GetHit(int num)
    {
        //进入受攻击状态
        StateBase.state.GetHit(num);
    }

    public void OnAnimationHit()
    {
        Debug.Log(StateBase.state.GetType().Name);
        StateBase.state?.Hit();
    }
}
