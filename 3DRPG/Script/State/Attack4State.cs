using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack4State : StateBase
{
    private bool triggerHit;

    private void OnEnable()
    {
        triggerHit = true;
        finishTime = 1.1f;
        autoFinish = true;
        animator.SetBool("Attack4", true);
    }

    private void OnDisable()
    {
        animator.SetBool("Attack4", false);
    }

    public override void Hit()
    {
        if (!triggerHit) return;

        triggerHit = false;

        Collider[] colliders = Physics.OverlapSphere(player.transform.position, 2f);

        foreach (Collider collider in colliders)
        {
            EnemyControl enemy = collider.GetComponent<EnemyControl>();
            if (enemy == null) continue;

            Vector3 dir = (enemy.transform.position - player.transform.position).normalized;
            dir.y = 0;

            float angle = Vector3.Angle(animator.transform.forward, dir);

            Debug.Log("In attack range");

            if (angle <= 45f)
            {
                enemy.GetHit(player.Attack * 3);
                Debug.Log($"Heavy hit! Damage: {player.Attack * 3}");
            }
        }
    }

    protected override void Update()
    {
        base.Update();

        if (finishTime <= 0f)
        {
            ChangeState<IdleState>();
        }
    }
}
