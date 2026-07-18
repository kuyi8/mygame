using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateBase
{
    //进入跑步状态
    private void OnEnable()
    {
        //播放跑步动画
        animator.SetBool("Run", true);
    }

    //离开跑步状态
    private void OnDisable()
    {
        //结束跑步动画
        animator.SetBool("Run", false);
    }

    protected override void Update()
    {
        base.Update();
        //按下攻击键
        if (InputManager.Instance.Attack1)
        {
            //切换到攻击状态
            ChangeState<Attack1State>();
        }
        //按下重攻击键
        if (InputManager.Instance.Attack2)
        {
            //切换到重攻击状态
            ChangeState<Attack4State>();
        }
        //按下交互键
        if (InputManager.Instance.Interactive)
        {
            //切换到交互状态
            ChangeState<InteractiveState>();
        }
        //按下跳跃键
        if (InputManager.Instance.Jump)
        {
            //切换到跳跃状态
            ChangeState<JumpState>();
        }
        //松开移动键
        if (InputManager.Instance.Move == Vector2.zero)
        {
            //切换到站立状态
            ChangeState<IdleState>();
        }
        else
        {
            //获得摇杆向量
            var dir = Quaternion.LookRotation(GetComponentInChildren<Camera>().transform.forward) * new Vector3(InputManager.Instance.Move.x, 0, InputManager.Instance.Move.y).normalized;
            //让角色朝向摇杆向量
            GetComponentInChildren<Animator>().transform.rotation = Quaternion.LookRotation(dir);
            //向摇杆方向移动
            transform.position += dir * 5 * Time.deltaTime;
        }
    }
}
