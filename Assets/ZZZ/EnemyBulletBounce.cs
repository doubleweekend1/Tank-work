using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyBulletBounce : MonoBehaviour
{
    [Header("最大反弹次数")]
    public int maxBounceCount = 4;
    private int currentBounceCount = 0;
    [Header("子弹参数")]
    public float speed = 10f;
    public float leastspeed = 8;
    [Header("反弹参数")]
    public float bounceDamping = 1f;  // 1 = 完美反弹，0.9 = 损失10%能量
    public LayerMask bounceLayers;     // 可反弹的层（在Inspector中设置）

    private Rigidbody rb;
    private Vector3 lastVelocity;
    private int inbounce;
    void OnEnable()
    {
        inbounce = 0;
        rb = GetComponent<Rigidbody>();
        lastVelocity = rb.velocity;

        //rb.velocity = transform.forward * speed;
        currentBounceCount = 0;
        // 记录初始速度
        lastVelocity = rb.velocity; 
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        // 设置子弹的碰撞检测
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // 设置子弹的Layer（可选）
        //gameObject.layer = LayerMask.NameToLayer("EnemyBullet");
        UpdateRotationByVelocity();
    }
    void UpdateRotationByVelocity()
    {
        if (rb.velocity.magnitude > 0.01f)
        {
            transform.forward = rb.velocity.normalized;
        }
    }
    void FixedUpdate()
    {
        // 记录上一帧的速度（用于反弹计算）
        lastVelocity = rb.velocity;
        if (lastVelocity.sqrMagnitude < leastspeed * leastspeed)
        {
            FindObjectOfType<BulletPool>().ReturnBullet(gameObject);
        }
        UpdateRotationByVelocity();
    }

    /*void OnCollisionEnter(Collision collision)
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
    }*/
    void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞的物体是否在反弹层中
        if (IsBounceLayer(collision.gameObject.layer))
        {
            // 计算完美反弹方向
            if (inbounce == 1)
            {
                return;
            }
            currentBounceCount++;
            //Debug.Log("bounce+1");
            inbounce = 1;
            Invoke("canbounce", 0.015f);
            // 超过最大次数 → 销毁子弹
            if (currentBounceCount > maxBounceCount)
            {
                Debug.Log($"当前反弹次数: {currentBounceCount}, 最大次数: {maxBounceCount}");
                FindObjectOfType<EnemyBulletPool>().ReturnBullet(gameObject);
            }
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflection = Vector3.Reflect(lastVelocity.normalized, normal);

            // 应用新速度（保持速率不变）
            rb.velocity = reflection * speed;

            // 更新记录的速度和速率
            lastVelocity = rb.velocity;
            speed = rb.velocity.magnitude;
            UpdateRotationByVelocity();
        }
    }
    // 检查是否为反弹层FindObjectOfType<EnemyBulletPool>().ReturnBullet(gameObject);
    void canbounce()
    {
        inbounce = 0;
    }
    // 检查是否为反弹层FindObjectOfType<BulletPool>().ReturnBullet(gameObject);
    bool IsBounceLayer(int layer)
    {
        return ((1 << layer) & bounceLayers) != 0;
    }
}

