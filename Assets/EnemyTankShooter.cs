using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyFireType
{
    Fire1,
    Fire2,
    Fire3,
    Fire4
}
public class EnemyTankShooter : MonoBehaviour
{
    [Header("检测设置")]
    public float detectRange = 15f;      // 识别范围
    public float attackRange = 12f;      // 开火范围
    public LayerMask playerLayer;        // 玩家所在层

    [Header("射击设置")]
    public Transform firePoint;           // 子弹发射点（炮口位置）
    public float fireRate = 1f;           // 每秒发射次数
    public float bulletSpeed = 20f;

    [Header("炮塔设置")]
    public Transform turret;              // 炮塔Transform（拖拽赋值）

    [Header("当前开火模式")]
    public EnemyFireType currentFireType;

    // 引用你的4个开火脚本
    public EnemyTankFire1 EtankFire1;
    //public EnemyTankFire2 EtankFire2;
   // public EnemyTankFire3 EtankFire3;
   // public EnemyTankFire4 EtankFire4;

    private Transform player;
    private float nextFireTime;

    void Start()
    {
        // 自动寻找玩家
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("没有找到Tag为'Player'的对象！");

        // 如果没有手动指定炮塔，尝试自动查找名为"Turret"的子物体
        if (turret == null)
            turret = transform.Find("Turret");

        if (turret == null)
            Debug.LogWarning("未找到炮塔Transform，请手动拖拽赋值");
    }

    void Update()
    {
        if (player == null || turret == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectRange)
        {
            // 1. 炮塔瞬时转向玩家
            InstantTurnTurret();

            // 2. 如果在开火范围内，发射子弹
            if (distanceToPlayer <= attackRange)
            {
                FireByCurrentType();
            }
        }
    }

    /// <summary>
    /// 炮塔瞬时转向玩家（无旋转过程）
    /// </summary>
    void InstantTurnTurret()
    {
        // 计算指向玩家的方向
        Vector3 directionToPlayer = player.position - turret.position;
        directionToPlayer.y = 0;  // 只绕Y轴旋转，炮管保持水平

        if (directionToPlayer != Vector3.zero)
        {
            // 直接设置炮塔朝向目标方向（瞬时）
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            turret.rotation = targetRotation;
        }
    }

    /*void ShootAtPlayer()
    {
        if (Time.time < nextFireTime) return;
        if (firePoint == null || bulletPrefab == null) return;

        nextFireTime = Time.time + (1f / fireRate);

        // 生成子弹
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 给子弹添加速度
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        // 自动销毁子弹
        Destroy(bullet, 5f);
    }*/
    void FireByCurrentType()
    {
        if (Time.time < nextFireTime) return;
        if (firePoint == null ) return;

        nextFireTime = Time.time + (1f / fireRate);
        switch (currentFireType)
        {
            case EnemyFireType.Fire1:
                EtankFire1.Fire();
                break;
            /*case EnemyFireType.Fire2:
                tankFire2.Fire();
                break;
                case EnemyFireType.Fire3:
                    tankFire3.Fire();
                    break;
                case EnemyFireType.Fire4:
                    tankFire4.Fire();
                    break;
                */
        }
    }
    // 可视化识别范围（Scene视图中显示）
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}