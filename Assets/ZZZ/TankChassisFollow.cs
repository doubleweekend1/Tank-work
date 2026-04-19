using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankChassisFollow : MonoBehaviour
{
    [Header("底盘设置")]
    public Transform chassisTransform; // 拖入你的底盘模型
    public float followSpeed = 8f;    // 跟随转向速度

    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 velocity = _rb.velocity;

        // 只有速度大于阈值才转动，不然停死不抖
        if (velocity.magnitude > 0.1f)
        {
            // 让底盘朝向移动方向
            Quaternion targetRot = Quaternion.LookRotation(velocity);
            chassisTransform.rotation = Quaternion.Lerp(
                chassisTransform.rotation,
                targetRot,
                followSpeed * Time.fixedDeltaTime
            );
        }
    }
}