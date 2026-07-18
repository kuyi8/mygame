using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    private PlayerControl player;

    private void Awake()
    {
        // 找到 Player 上的 PlayerControl
        player = GetComponentInParent<PlayerControl>();
    }

    // 供动画事件调用
    public void Hit()
    {
        if (player != null)
        {
            player.OnAnimationHit();
        }
    }
}
