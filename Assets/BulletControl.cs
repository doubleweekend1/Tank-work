using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 50f;  // 存在时间（秒）
    private float timer;
    private BulletPool pool;

    void Start()
    {
        pool = FindObjectOfType<BulletPool>();
    }

    void OnEnable()
    {
        // 每次从池子取出时重置计时器
        timer = 0f;
    }

    void Update()
    {
        // 计时
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            ReturnToPool();  // 时间到，归还到池子
        }
    }

    void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnBullet(gameObject);
        }
        else
        {
            Destroy(gameObject);  // 保底销毁
        }
    }
}