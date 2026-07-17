using UnityEngine;

public class IdleState : StateBase
{
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

        if (InputManager.Instance.Move != Vector2.zero)
            ChangeState<RunState>();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed");
            StateBase.state.GetHit(3);
        }
    }
}
