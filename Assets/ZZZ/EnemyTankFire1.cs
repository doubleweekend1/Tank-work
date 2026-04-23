using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyTankFire1 : MonoBehaviour
{
    //public string targettag;
    public TurretRecoil turret;
    [Header("发射点")]
    public Transform firePoint;
    [Header("子弹速度")]
    public float bulletSpeed = 10f;
    private EnemyBulletPool bulletPool;
    public int bouncetimes;
    private void Start()
    {
        bulletPool = FindObjectOfType<EnemyBulletPool>();
        if (bulletPool == null)
        {
            Debug.LogError("场景中没有 EnemyBulletPool！");
        }
    }
    public void Fire()
    {
        // 在枪口生成子弹
        // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet = bulletPool.GetBullet(firePoint.position, firePoint.rotation);
        EnemyBulletBounce ABt = bullet.GetComponent<EnemyBulletBounce>();
        ABt.maxBounceCount = bouncetimes;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }
        if (turret != null)
        {
            turret.DoRecoil();
        }
        
        // 3秒后销毁，防止内存卡死
        //Destroy(bullet, 50f);
        //bulletPool.ReturnBullet(bullet);
    }
}
