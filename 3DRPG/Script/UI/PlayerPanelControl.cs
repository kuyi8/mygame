using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelControl : MonoBehaviour
{
    public static PlayerPanelControl Instance;
    public Image[] hpImages;

    void Start()
    {
        Instance = this;
    }

    public void SetHp(int hp)
    {
        foreach (var hpImage in hpImages)
        {
            hpImage.fillAmount = 0;
        }

        if (hp < 0) hp = 0;
        if (hp > 10) hp = 10;

        int count = hp / 2;
        int half = hp % 2;

        for (int i = 0; i < count; i++)
        {
            hpImages[i].fillAmount = 1;
        }

        if (half != 0)
        {
            hpImages[count].fillAmount = 0.5f;
        }
    }
}
