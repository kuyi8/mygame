using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{
    private PlayerControl player;

    private void Awake()
    {
        player = GetComponentInParent<PlayerControl>();
    }

    public void Hit()
    {
        if (player != null)
        {
            player.OnAnimationHit();
        }
    }
}
