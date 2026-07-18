using UnityEngine;

public class StateBase : MonoBehaviour
{
    //当前的状态
    public static StateBase state;
    //动画控制器
    protected Animator animator;
    //刚体
    protected Rigidbody rbody;
    //是否会自动结束状态
    protected bool autoFinish = false;
    //多长时间后自动结束当前状态，-1不结束
    protected float finishTime = 0;
    //多长时间后可以切换下一个状态，后摇
    protected float changeTime = 0.5f;
    //玩家控制器
    protected PlayerControl player;
    //是否允许受攻击
    protected bool getHit = true;

    //状态初始化
    void Awake()
    {
        //获取角色身上的动画控制器与导航代理
        animator = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody>();
        player = GetComponent<PlayerControl>();
    }

    //切换状态
    public void ChangeState<T>() where T : StateBase
    {
        //获取要切换的状态
        state = GetComponent<T>();
        //关闭当前状态
        this.enabled = false;
        //开启新状态
        state.enabled = true;
    }

    //受到伤害
    public void GetHit(int num)
    {
        //如果该状态不支持受伤
        if (getHit == false)
        {
            //跳出状态处理
            return;
        }
        //玩家受到伤害
        player.hp -= num;
        //如果血量小于等于0
        if (player.hp <= 0)
        {
            Debug.Log("Hp <= 0");
            //死亡，进入死亡状态
            ChangeState<DieState>();
        }
        else
        {
            Debug.Log("HP > 0");
            //受伤害，进入受攻击状态
            ChangeState<DamageState>();
        }
    }

    protected virtual void Update()
    {
        //自动结束状态的倒计时
        finishTime -= Time.deltaTime;
        //进入后摇阶段的倒计时
        changeTime -= Time.deltaTime;
        //Debug.Log(GetType().Name);
    }

    public virtual void Hit()       //攻击函数
    {
    }
}