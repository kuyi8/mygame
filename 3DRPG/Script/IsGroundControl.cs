using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGroundControl : MonoBehaviour
{
    private bool isGround;
    public bool IsGround
    {
        get
        {
            return isGround;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isGround = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGround = false;
    }
}
