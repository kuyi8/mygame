using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        var x = InputManager.Instance.Camera.x;
        transform.RotateAround(player.position, player.up, x * 180 * Time.deltaTime);
    }
}
