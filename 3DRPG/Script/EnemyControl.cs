using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int hp = 10;

    public void Hit()
    {
        Debug.Log("Hit");
    }

    public void GetHit(int num)
    {
        Debug.Log("GetHit");
    }
}
