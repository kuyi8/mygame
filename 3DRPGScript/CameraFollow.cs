using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [Header("跟随目标")]
    public Transform target;

    [Header("位置偏移 (相对目标)")]
    public Vector3 positionOffset = new Vector3(0, 5, -10);

    [Header("角度偏移 (欧拉角)")]
    public Vector3 rotationOffset = new Vector3(15, 0, 0);

    [Header("跟随平滑")]
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        // 计算期望位置：目标位置 + 偏移（目标本地坐标系 -> 世界？通常在世界空间）
        // 但为了支持目标旋转时偏移跟随，可以将偏移旋转到目标的方向
        Vector3 desiredPosition = target.position + target.rotation * positionOffset;
        // 或者仅仅世界偏移（简单场景）
        // Vector3 desiredPosition = target.position + positionOffset;

        // 平滑移动
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 计算期望旋转：目标旋转 * 角度偏移的旋转
        Quaternion desiredRotation = target.rotation * Quaternion.Euler(rotationOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed * Time.deltaTime);
    }
}
