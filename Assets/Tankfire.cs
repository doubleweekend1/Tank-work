using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire : MonoBehaviour
{
    [Header("子弹预制体")]
    public GameObject bulletPrefab;
    [Header("发射点")]
    public Transform firePoint;
    [Header("子弹速度")]
    public float bulletSpeed = 10f;

    void Update()
    {
        // 鼠标左键发射
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // 在枪口生成子弹
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;

        // 3秒后销毁，防止内存卡死
        Destroy(bullet, 3f);
    }
}
