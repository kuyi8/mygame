using UnityEngine;
using UnityEngine.Tilemaps;

public class IdleState : StateBase
{
    protected override void Update()
    {
        base.Update();
        //按下轻攻击按键
        if (InputManager.Instance.Attack1)
        {
            //切换到轻攻击1
            ChangeState<Attack1State>();
        }
        //按下重攻击按键
        if (InputManager.Instance.Attack2)
        {
            //切换到重攻击
            ChangeState<Attack4State>();
        }
        //按下交互按键
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
        //按下移动键
        if (InputManager.Instance.Move != Vector2.zero)
        {
            //切换到移动状态
            ChangeState<RunState>();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 按下 R 键时执行一次
            Debug.Log("R 键被按下");
            //player.GetHit(3);
            StateBase.state.GetHit(3);
        }
    }
}