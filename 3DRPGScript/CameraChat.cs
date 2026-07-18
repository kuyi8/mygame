using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChat : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private float rotateSpeed = 180f;

    private Vector3 offset;

    private float yaw;

    void Start()
    {
        player = transform.parent;

        // 记录初始偏移
        offset = transform.localPosition;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float x = InputManager.Instance.Camera.x;

        yaw += x * rotateSpeed * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(0, yaw, 0);

        // 绕角色旋转
        transform.position = player.position + rotation * offset;

        // 始终看向角色
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
