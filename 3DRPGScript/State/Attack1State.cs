using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1State : StateBase
{
    // 是否已经造成过伤害（防止一次攻击触发多次）
    private bool triggerHit;

    private void OnEnable()
    {
        // 本次攻击允许造成伤害
        triggerHit = true;

        // 动画持续时间
        finishTime = 1.01f;

        // 自动结束状态
        autoFinish = true;

        // 播放攻击动画
        animator.SetBool("Attack1", true);
    }

    private void OnDisable()
    {
        animator.SetBool("Attack1", false);
    }

    /// <summary>
    /// 这个函数由 Animation Event 调用
    /// </summary>
    public override void Hit()
    {
        Debug.Log("Attack1 Hit 被调用");
        // 防止同一次攻击造成多次伤害
        if (!triggerHit)
            return;

        triggerHit = false;

        // 检测周围敌人
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 2f);

        foreach (Collider collider in colliders)
        {
            EnemyControl enemy = collider.GetComponent<EnemyControl>();

            if (enemy == null)
                continue;

            // 玩家 -> 敌人的方向
            Vector3 dir = (enemy.transform.position - player.transform.position).normalized;
            dir.y = 0;

            // 玩家朝向 与 敌人方向 的夹角
            float angle = Vector3.Angle(animator.transform.forward, dir);

            Debug.Log("进入攻击范围");

            // 前方90°扇形（左右各45°）
            if (angle <= 45f)
            {
                enemy.GetHit(player.Attack);
                Debug.Log($"攻击成功造成{player.Attack}点伤害");
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (finishTime < 0.4f && triggerHit == true)
        { 
            triggerHit= false;
            Hit();
        }

        
        if (changeTime <= 0 && InputManager.Instance.Attack1)
        {
            animator.SetBool("1,2", true);
            ChangeState<Attack2State>();
        }

        // 动画结束，切回Idle
        if (finishTime <= 0f)
        {
            ChangeState<IdleState>();
        }
    }
}
