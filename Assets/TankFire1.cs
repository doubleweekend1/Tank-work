using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TankFire1 : MonoBehaviour
{
    public TurretRecoil turret;
    [Header("子弹预制体")]
    public GameObject bulletPrefab;
    [Header("发射点")]
    public Transform firePoint;
    [Header("子弹速度")]
    public float bulletSpeed = 10f;
    private BulletPool bulletPool;
    private void Start()
    {
        bulletPool = FindObjectOfType<BulletPool>();
        if (bulletPool == null)
        {
            Debug.LogError("场景中没有 BulletPool！");
        }
    }
    /*void Update()
    {
        // 鼠标左键发射
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    */
    public void Fire()
    {
        // 在枪口生成子弹
       // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet = bulletPool.GetBullet(firePoint.position,  firePoint.rotation  );
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
        turret.DoRecoil();
        // 3秒后销毁，防止内存卡死
        //Destroy(bullet, 50f);
        //bulletPool.ReturnBullet(bullet);
    }
}
