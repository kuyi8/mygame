using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveState : StateBase
{
    private void OnEnable()
    {
        var colliders = Physics.OverlapSphere(transform.position, 2);

        foreach (var collider in colliders)
        {
            InteractiveBase tmp = collider.GetComponent<InteractiveBase>();
            if (tmp != null)
            {
                tmp.Use();
                break;
            }
        }

        ChangeState<IdleState>();
    }

    private void OnDisable()
    {
    }
}
