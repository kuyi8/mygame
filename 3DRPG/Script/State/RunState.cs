using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : StateBase
{
    private void OnEnable()
    {
        animator.SetBool("Run", true);
    }

    private void OnDisable()
    {
        animator.SetBool("Run", false);
    }

    protected override void Update()
    {
        base.Update();

        if (InputManager.Instance.Attack1)
            ChangeState<Attack1State>();

        if (InputManager.Instance.Attack2)
            ChangeState<Attack4State>();

        if (InputManager.Instance.Interactive)
            ChangeState<InteractiveState>();

        if (InputManager.Instance.Jump)
            ChangeState<JumpState>();

        if (InputManager.Instance.Move == Vector2.zero)
        {
            ChangeState<IdleState>();
        }
        else
        {
            var dir = Quaternion.LookRotation(GetComponentInChildren<Camera>().transform.forward)
                * new Vector3(InputManager.Instance.Move.x, 0, InputManager.Instance.Move.y).normalized;

            GetComponentInChildren<Animator>().transform.rotation = Quaternion.LookRotation(dir);
            transform.position += dir * 5 * Time.deltaTime;
        }
    }
}
