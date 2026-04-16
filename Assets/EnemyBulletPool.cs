using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        // 提前创建20发子弹，全部禁用
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            pool.Enqueue(bullet);
        }
    }

    // 从池子里取一颗子弹
    public GameObject GetBullet(Vector3 position, Quaternion rotation)
    {
        GameObject bullet;

        if (pool.Count > 0)
        {
            // 有闲置：拿出来用
            bullet = pool.Dequeue();
        }
        else
        {
            // 没闲置：新建一个（应急）
            bullet = Instantiate(bulletPrefab);
        }

        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.SetActive(true);
        return bullet;
    }

    // 子弹用完后调用这个方法归还
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}