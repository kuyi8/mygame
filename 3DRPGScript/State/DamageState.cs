using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : StateBase
{
    private void OnEnable()
    {
        //设置结束时间，一般根据动画播放时间来设置该项
        finishTime = 0.4f;
        //设置自动结束该状态
        autoFinish = true;
        //播放受到攻击动画
        animator.SetBool("GetHit", true);
    }

    private void OnDisable()
    {
        //停止播放受到攻击动画
        animator.SetBool("GetHit", false);
    }

    protected override void Update()
    {
        base.Update();
        //如果倒计时到达
        if (finishTime <= 0)
        {
            //切换到站立状态
            ChangeState<IdleState>();
        }
    }
}
