using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMove : MonoBehaviour
{
    public float moveSpeed = 8f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // 防止倒地：锁定旋转
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // 获取 WASD
        float h = Input.GetAxis("Horizontal"); // A D → X
        float v = Input.GetAxis("Vertical");   // W S → Z

        // 直接用【世界坐标系】Vector3
        Vector3 dir = new Vector3(h, 0, v);
        // 归一化，防止斜着走更快
        if (dir.magnitude > 1f)
            dir.Normalize();

        Vector3 V = dir * moveSpeed;
        rb.velocity = V;
    }
    void Update()
    {
        
    }
}
