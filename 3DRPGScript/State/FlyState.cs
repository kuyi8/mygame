using UnityEngine;

public class FlyState : StateBase
{
    public GameObject Wing;
    private IsGroundControl isGround;
    private Vector3 currentVelocity;
    float yVelocity = 0f;
    float gravity = -3f;
    SphereCollider sphereCollider;
    private void OnEnable()
    {
        //轻功也使用跳跃动画，你也可以找一个轻功或飞翔的动画效果会更好
        animator.SetBool("Jump", true);
        //设置刚体质量，人就会“轻”了
        rbody.mass = 0.3f;
        //给一个向上的力
        rbody.AddForce(Vector3.up * 200);
        //获取判断是否接触地面的脚本
        isGround = GetComponentInChildren<IsGroundControl>();
        //激活翅膀显示
        //animator.transform.Find("FireWing").gameObject.SetActive(true);
        Wing.SetActive(true);
        sphereCollider = GetComponentInChildren<SphereCollider>();
        sphereCollider.radius = 0.3f;
    }

    private void OnDisable()
    {
        //停止跳跃动画
        animator.SetBool("Jump", false);
        //恢复刚体质量
        rbody.mass = 1;
        //取消翅膀显示
        //animator.transform.Find("FireWing").gameObject.SetActive(false);
        Wing.SetActive(false);
        sphereCollider.radius = 0.02f;
    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log($"下落速度：{rbody.velocity.y}");
        Debug.Log(isGround.IsGround);

       // 应用重力
        yVelocity += gravity * Time.deltaTime;

        // 更新 Y 坐标
        transform.position += Vector3.up * yVelocity * Time.deltaTime;
        

        //如果碰到地面
        if (isGround.IsGround)
        {
            yVelocity = 0f;
            //切换到站立状态
            ChangeState<IdleState>();
        }

        //允许在跳跃中移动
        if (InputManager.Instance.Move != Vector2.zero)
        {
            //获得摇杆向量
            var dir = Quaternion.LookRotation(GetComponentInChildren<Camera>().transform.forward)
                * new Vector3(InputManager.Instance.Move.x, 0, InputManager.Instance.Move.y).normalized;
            //让角色朝向摇杆向量
            GetComponentInChildren<Animator>().transform.rotation = Quaternion.LookRotation(dir);
            //向摇杆方向移动
            transform.position += dir * 4 * Time.deltaTime;
            //rbody.AddForce(dir * 4, ForceMode.Force);
        }

    }

    protected void LateUpdate()
    {
        
    }
}