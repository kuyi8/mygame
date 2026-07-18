using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [Header("必填：拖入Slider组件")]
    public Slider healthSlider;

    void Start()
    {
        // 如果没有手动拖入，尝试获取自身组件
        if (healthSlider == null)
            healthSlider = GetComponent<Slider>();

        // 初始化 Slider 范围
        if (healthSlider != null && PlayerControl.Instance != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = PlayerControl.Instance.MaxHp;
            healthSlider.value = PlayerControl.Instance.Hp;
        }
    }

    void Update()
    {
        // 每帧更新血条显示
        if (healthSlider != null && PlayerControl.Instance != null)
        {
            // 确保 Slider 的 maxValue 和当前血量一致（如果最大血量会变化则更新）
            if (healthSlider.maxValue != PlayerControl.Instance.MaxHp)
                healthSlider.maxValue = PlayerControl.Instance.MaxHp;

            healthSlider.value = PlayerControl.Instance.Hp;
        }
    }

    // 可选：如果外部需要手动刷新，可以调用此方法
    public void Refresh()
    {
        if (healthSlider != null && PlayerControl.Instance != null)
        {
            healthSlider.maxValue = PlayerControl.Instance.MaxHp;
            healthSlider.value = PlayerControl.Instance.Hp;
        }
    }
}
