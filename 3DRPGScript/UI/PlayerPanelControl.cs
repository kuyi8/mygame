using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelControl : MonoBehaviour
{
    //设置单例，方便外面调用
    public static PlayerPanelControl Instance;
    //5个红心图像
    public Image[] hpImages;

    void Start()
    {
        //单例设置
        Instance = this;
    }

    //设置血量
    public void SetHp(int hp)
    {
        //清空现有血量显示
        foreach (var hpImage in hpImages)
        {
            hpImage.fillAmount = 0;
        }

        //做一个简单的血量限制
        //当设置的血量小于0
        if (hp < 0)
        {
            //纠正血量为0
            hp = 0;
        }
        //当设置的血量大于10
        if (hp > 10)
        {
            //纠正血量为10
            hp = 10;
        }

        //显示完整红心的个数
        int count = hp / 2;
        //显示半个红心的个数
        int half = hp % 2;

        //遍历完整红心个数
        for (int i = 0; i < count; i++)
        {
            //显示完整红心
            hpImages[i].fillAmount = 1;
        }
        //如果有半个红心，显示半个红心
        if (half != 0)
        {
            //显示半个红心
            hpImages[count].fillAmount = 0.5f;
        }
    }
}
