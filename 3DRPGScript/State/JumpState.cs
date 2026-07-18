using UnityEngine;

public class JumpState : StateBase
{
    private void OnEnable()
    {
        //设置结束时间，一般根据动画播放时间来设置该项
        finishTime = 0.6f;
        //设置自动结束该状态
        autoFinish = true;
        //播放跳跃动画
        animator.SetBool("Jump", true);
        //给主角一个跳跃力
        rbody.AddForce(Vector3.up * 200);
    }

    private void OnDisable()
    {
        //停止播放跳跃动画
        animator.SetBool("Jump", false);
    }

    //每帧调用一次Update
    protected override void Update()
    {
        base.Update();
        Debug.Log($"下落速度：{rbody.velocity.y}");
        //如果倒计时到达
        if (finishTime <= 0)
        {
            //切换到站立状态
            ChangeState<IdleState>();
        }

        //允许在起跳后，再次按下跳跃按钮施展轻功
        if (finishTime > 0.2f && InputManager.Instance.Jump)
        {
            //切换到轻功状态
            ChangeState<FlyState>();
        }

        //允许在跳跃中移动
        if (InputManager.Instance.Move != Vector2.zero)
        {
            //获得摇杆向量
            var dir = Quaternion.LookRotation(GetComponentInChildren<Camera>().transform.forward) * new Vector3(InputManager.Instance.Move.x, 0, InputManager.Instance.Move.y).normalized;
            //让角色朝向摇杆向量
            GetComponentInChildren<Animator>().transform.rotation = Quaternion.LookRotation(dir);
            //向摇杆方向移动
            transform.position += dir * 3 * Time.deltaTime;
        }
    }
}