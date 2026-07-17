using UnityEngine;

public class StateBase : MonoBehaviour
{
    public static StateBase state;

    protected Animator animator;
    protected Rigidbody rbody;
    protected bool autoFinish = false;
    protected float finishTime = 0;
    protected float changeTime = 0.5f;
    protected PlayerControl player;
    protected bool getHit = true;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody>();
        player = GetComponent<PlayerControl>();
    }

    public void ChangeState<T>() where T : StateBase
    {
        state = GetComponent<T>();
        this.enabled = false;
        state.enabled = true;
    }

    public void GetHit(int num)
    {
        if (getHit == false) return;

        player.hp -= num;
        if (player.hp <= 0)
        {
            Debug.Log("Hp <= 0");
            ChangeState<DieState>();
        }
        else
        {
            Debug.Log("HP > 0");
            ChangeState<DamageState>();
        }
    }

    protected virtual void Update()
    {
        finishTime -= Time.deltaTime;
        changeTime -= Time.deltaTime;
    }

    public virtual void Hit()
    {
    }
}
