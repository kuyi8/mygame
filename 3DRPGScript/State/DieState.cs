using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : StateBase
{
    public GameObject Healing;
    float time;
    private void OnEnable()
    {
        //播放跑步动画
        animator.SetBool("Die", true);
        //Healing.SetActive(true);
        time = 0f;
    }

    private void OnDisable()
    {
        //播放跑步动画
        animator.SetBool("Die", false);
        Healing.SetActive(false);
        player.hp = player.MaxHp;
    }
    void Start()
    {
        //time = 0f;
    }

    // Update is called once per frame
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
