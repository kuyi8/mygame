using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : StateBase
{
    private void OnEnable()
    {
        finishTime = 0.4f;
        autoFinish = true;
        animator.SetBool("GetHit", true);
    }

    private void OnDisable()
    {
        animator.SetBool("GetHit", false);
    }

    protected override void Update()
    {
        base.Update();
        if (finishTime <= 0)
        {
            ChangeState<IdleState>();
        }
    }
}
