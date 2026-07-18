using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    [Header("旋转速度 (度/秒)")]
    [Tooltip("建议 20~60，数值越小转得越慢")]
    public float rotateSpeed = 30f;

    [Header("起始角度随机化")]
    public bool randomStartAngle = true;

    private void Start()
    {
        if (randomStartAngle)
        {
            // 随机初始朝向，避免所有物品方向一致
            float randomY = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0, randomY, 0);
        }
    }

    private void Update()
    {
        // 绕本地 Y 轴旋转，速度与帧率无关
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
