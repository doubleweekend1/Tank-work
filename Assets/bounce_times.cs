using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet_Bounce : MonoBehaviour
{
    [Header("最大反弹次数")]
    public int maxBounceCount = 4;
    private int currentBounceCount = 0;
    [Header("子弹参数")]
    public float speed = 10f;

    [Header("反弹参数")]
    public float bounceDamping = 1f;  // 1 = 完美反弹，0.9 = 损失10%能量
    public LayerMask bounceLayers;     // 可反弹的层（在Inspector中设置）

    private Rigidbody rb;
    private Vector3 lastVelocity;
    private bool hasBounced = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastVelocity = rb.velocity;

        //rb.velocity = transform.forward * speed;

        // 记录初始速度
        lastVelocity = rb.velocity;
        // 设置子弹的碰撞检测
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // 设置子弹的Layer（可选）
        gameObject.layer = LayerMask.NameToLayer("Bullet");
    }

    void FixedUpdate()
    {
        // 记录上一帧的速度（用于反弹计算）
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞的物体是否在反弹层中
        if (IsBounceLayer(collision.gameObject.layer))
        {
            // 计算完美反弹方向
            currentBounceCount++;
            // 超过最大次数 → 销毁子弹
            if (currentBounceCount >= maxBounceCount)
            {
                FindObjectOfType<BulletPool>().ReturnBullet(gameObject);
            }
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflection = Vector3.Reflect(lastVelocity.normalized, normal);

            // 应用新速度（保持速率不变）
            rb.velocity = reflection * speed;

            // 更新记录的速度和速率
            lastVelocity = rb.velocity;
            speed = rb.velocity.magnitude;
        }
    }
    // 检查是否为反弹层FindObjectOfType<BulletPool>().ReturnBullet(gameObject);
    bool IsBounceLayer(int layer)
    {
        return ((1 << layer) & bounceLayers) != 0;
    }
}

