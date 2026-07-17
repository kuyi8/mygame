using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public float rotateSpeed = 30f;
    public bool randomStartAngle = true;

    private void Start()
    {
        if (randomStartAngle)
        {
            float randomY = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0, randomY, 0);
        }
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
