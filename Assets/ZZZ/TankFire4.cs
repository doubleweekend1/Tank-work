using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire4 : MonoBehaviour
{
    public string TargetTag;
    // 在Inspector面板中拖拽指定各个Layer
    public LayerMask wallLayers;   // 勾选 RedWall 和 NormalWall 层
    public LayerMask enemyLayer;   // 勾选 Enemy 层
    public float laserMaxLength = 100f; // 激光最大长度

    // 关键修改：指定炮口位置（作为子物体引用）
    public Transform firePoint;          // 激光发射点（炮口位置）
    public Transform turretTransform;    // 炮塔的Transform（用于获取射击方向）

    public LineRenderer lineRenderer;    // 激光美术效果组件

    public void Fire()
    {

            ShootLaser();

    }

    void ShootLaser()
    {
        if (firePoint == null)
        {
            Debug.LogError("LaserShoot: 未指定 firePoint（炮口位置）！");
            return;
        }

        // 1. 确定射击方向
        Vector3 shootDirection;
            // 方案B：使用炮塔朝向（如果有炮塔）
            shootDirection = turretTransform.forward;
        // 2. 定义射线 (起点: 炮口)
        Ray ray = new Ray(firePoint.position, shootDirection);

        // 3. 获取射线路径上的所有物体
        RaycastHit[] allHits = Physics.RaycastAll(ray, laserMaxLength);

        // 用于记录激光应该停止的碰撞点（默认最大距离）
        float laserEndDistance = laserMaxLength;
        // 存储所有被击中的敌人
        List<RaycastHit> hitEnemies = new List<RaycastHit>();

        // 4. 筛选命中结果：按距离排序
        System.Array.Sort(allHits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in allHits)
        {
            // 检查是否碰到墙壁（阻挡物）
            if (((1 << hit.collider.gameObject.layer) & wallLayers) != 0)
            {
                // 碰到墙壁，激光在此终止
                laserEndDistance = hit.distance;
                flexible_wall FW = hit.collider.GetComponent<flexible_wall>();
                // 检查是否是 GreyWall 标签
                if (FW!=null)
                {          
                    if (FW != null)
                    {
                        FW.pubHide();
                    }
                }

                break; // 停止继续穿透检测
            }

            // 如果还没有碰到墙壁，检查是否是敌人
            if (((1 << hit.collider.gameObject.layer) & enemyLayer) != 0)
            {
                hitEnemies.Add(hit);
            }
        }

        // 5. 对所有路径上的敌人执行 Fun() 函数
        foreach (RaycastHit enemyHit in hitEnemies)
        {
            if (!enemyHit.collider.CompareTag(TargetTag))
            {
                continue;
            }
            // 尝试获取 Enemy 组件（按你的要求，用Tag识别也行）
            HP enemy = enemyHit.collider.GetComponent<HP>();
            if (enemy != null)
            {
                enemy.TakeDamage(15);
                Debug.Log("击中敌人: " + enemyHit.collider.name);
            }            
        }
        // 6. 更新美术效果 (LineRenderer)
        UpdateLaserVisual(laserEndDistance, shootDirection);
    }

    void UpdateLaserVisual(float distance, Vector3 direction)
    {
        if (lineRenderer == null) return;

        // 设置LineRenderer的端点：起点到碰撞点
        Vector3 startPoint = firePoint.position;
        Vector3 endPoint = firePoint.position + direction * distance;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        // 可选：让激光显示一小段时间然后消失
        CancelInvoke(nameof(HideLaser));
        Invoke(nameof(HideLaser), 0.05f);
    }

    void HideLaser()
    {
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }
}
