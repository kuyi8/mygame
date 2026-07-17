using UnityEngine;

public class JumpState : StateBase
{
    private void OnEnable()
    {
        finishTime = 0.6f;
        autoFinish = true;
        animator.SetBool("Jump", true);
        rbody.AddForce(Vector3.up * 200);
    }

    private void OnDisable()
    {
        animator.SetBool("Jump", false);
    }

    protected override void Update()
    {
        base.Update();
        Debug.Log($"Velocity Y: {rbody.velocity.y}");

        if (finishTime <= 0)
            ChangeState<IdleState>();

        if (finishTime > 0.2f && InputManager.Instance.Jump)
            ChangeState<FlyState>();

        if (InputManager.Instance.Move != Vector2.zero)
        {
            var dir = Quaternion.LookRotation(GetComponentInChildren<Camera>().transform.forward)
                * new Vector3(InputManager.Instance.Move.x, 0, InputManager.Instance.Move.y).normalized;

            GetComponentInChildren<Animator>().transform.rotation = Quaternion.LookRotation(dir);
            transform.position += dir * 3 * Time.deltaTime;
        }
    }
}
