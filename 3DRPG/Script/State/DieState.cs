using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : StateBase
{
    public GameObject Healing;
    float time;

    private void OnEnable()
    {
        animator.SetBool("Die", true);
        time = 0f;
    }

    private void OnDisable()
    {
        animator.SetBool("Die", false);
        Healing.SetActive(false);
        player.hp = player.MaxHp;
    }

    protected override void Update()
    {
        time += Time.deltaTime;
        if (time > 1f)
        {
            Healing.SetActive(true);
        }

        if (time > 5f)
        {
            ChangeState<IdleState>();
        }
    }
}
