using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveState : StateBase
{
    private void OnEnable()
    {
        //这个状态可以播放一个交互的动作，这里就不播放了，直接开始交互
        //简单获取周围2米内的物体
        var colliders = Physics.OverlapSphere(transform.position, 2);
        //遍历周围物体
        foreach (var collider in colliders)
        {
            //获取交互物
            InteractiveBase tmp = collider.GetComponent<InteractiveBase>();
            //如果物体身上有交互物组件
            if (tmp != null)
            {
                //使用该交互物
                tmp.Use();
                //结束循环
                break;
            }
        }

        //切换到站立状态
        ChangeState<IdleState>();
    }

    private void OnDisable()
    {
        //Debug.Log("结束交互");
    }
}
