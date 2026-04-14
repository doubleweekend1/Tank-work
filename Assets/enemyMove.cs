using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float detectionRange = 20f;
    public float wanderRange = 10f;
    public float triggerDistance = 2f;
    private Rigidbody rb;
    private Transform player;
    private Vector3 wanderTarget;
    private bool isWandering = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        PickNewWanderTarget();
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < detectionRange)
        {
            // 发现玩家：追击
            isWandering = false;
            ChasePlayer();
        }
        else
        {
            // 未发现：游走
            isWandering = true;
            Wander();
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        rb.velocity = direction * moveSpeed;
        Vector3 toPlayer = player.position - transform.position;// 计算从当前物体指向玩家的方向（水平方向）
        float distance = toPlayer.magnitude;
        if (distance < triggerDistance)
        {
            toPlayer.y = 0; // 忽略高度差
            toPlayer.Normalize();

        // 计算当前速度在朝向玩家方向上的投影
            float speedTowardPlayer = Vector3.Dot(rb.velocity, toPlayer);

        // 消除朝向玩家的速度分量
            rb.velocity -= toPlayer * speedTowardPlayer;
         }




        // 炮塔转向玩家
        transform.rotation = Quaternion.LookRotation(direction);
    }

    void Wander()
    {
        // 到达目标点附近，选新目标
        if (Vector3.Distance(transform.position, wanderTarget) < 1f)
        {
            PickNewWanderTarget();
        }

        Vector3 direction = (wanderTarget - transform.position).normalized;
        direction.y = 0;
        rb.velocity = direction * moveSpeed;

        // 炮塔转向移动方向
        if (rb.velocity.magnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(rb.velocity);
    }
    //Armor_Skirt
    void PickNewWanderTarget()
    {
        Vector2 randomCircle = Random.insideUnitCircle * wanderRange;
        wanderTarget = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 撞墙或撞坦克 → 速度清零
        if (collision.gameObject.CompareTag("Tankwall") ||
            collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = Vector3.zero;

            // 如果是游走状态，立即选新目标
            if (isWandering)
            {
                PickNewWanderTarget();
            }
        }
    }
}