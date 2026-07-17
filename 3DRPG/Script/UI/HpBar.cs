using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider healthSlider;

    void Start()
    {
        if (healthSlider == null)
            healthSlider = GetComponent<Slider>();

        if (healthSlider != null && PlayerControl.Instance != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = PlayerControl.Instance.MaxHp;
            healthSlider.value = PlayerControl.Instance.Hp;
        }
    }

    void Update()
    {
        if (healthSlider != null && PlayerControl.Instance != null)
        {
            if (healthSlider.maxValue != PlayerControl.Instance.MaxHp)
                healthSlider.maxValue = PlayerControl.Instance.MaxHp;

            healthSlider.value = PlayerControl.Instance.Hp;
        }
    }

    public void Refresh()
    {
        if (healthSlider != null && PlayerControl.Instance != null)
        {
            healthSlider.maxValue = PlayerControl.Instance.MaxHp;
            healthSlider.value = PlayerControl.Instance.Hp;
        }
    }
}
