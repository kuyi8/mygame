using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;
    //[Header("跟随目标")]
    //public Transform target;
    //
    //[Header("位置偏移 (相对目标)")]
    //public Vector3 positionOffset = new Vector3(0, 5, -10);
    //
    //[Header("角度偏移 (欧拉角)")]
    //public Vector3 rotationOffset = new Vector3(15, 0, 0);
    //
    //[Header("跟随平滑")]
    //public float smoothSpeed = 5f;
    //public Vector3 targetLocalPos = new Vector3(0f, 3f, -5f);
    //public Vector3 targetLocalRot = new Vector3(15f, 0f, 0f);
    //public float smoothSpeed = 8f;
    void Start()
    {
        player = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

   
    void Update()
    {
        var x = InputManager.Instance.Camera.x;

        transform.RotateAround(player.position,player.up, x * 180 * Time.deltaTime);
    }
   //private void LateUpdate()
   //{
   //    // 平滑移动局部位置
   //    transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, smoothSpeed * Time.deltaTime);
   //    // 平滑旋转局部旋转
   //    Quaternion targetRot = Quaternion.Euler(targetLocalRot);
   //    transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, smoothSpeed * Time.deltaTime);
   //}
}
