using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [Header("基础属性")]
    public float maxHealth = 100;
    public float currentHealth;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    void Start()
    {
        currentHealth = MaxHealth;
    }

    // 普通子弹碰撞扣血
    /*private void OnCollisionEnter(Collision collision)
    {
        // 普通子弹标签设为 Bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(5);
            Destroy(collision.gameObject); // 普通子弹击中后消失
        }
    }
    */
    // 触发检测（声波用 Trigger）
    /*private void OnTriggerEnter(Collider other)
    {
        // 穿透声波标签设为 SonicWave
        if (other.CompareTag("SonicWave"))
        {
            // 拿到声波上的攻击值
            SonicWave sonic = other.GetComponent<SonicWave>();
            if (sonic != null)
            {
                TakeDamage(sonic.attack);
            }
        }
    }*/

    // 统一扣血方法
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 死亡逻辑
    void Die()
    {
        Debug.Log("敌方坦克已被摧毁");
        Destroy(gameObject);
    }
}
