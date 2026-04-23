using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [Header("基础属性")]
    public float maxHealth = 100;
    public float currentHealth;
    private int isUnbeatable;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public GameObject explosionPrefab;
    void Start()
    {
        isUnbeatable = 0;
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

    
    public void BecomeUnbeatable()
    {
        isUnbeatable = 1;
        Invoke("EndUnbeatable", 10f);
    }

    void EndUnbeatable()
    {
        isUnbeatable = 0;
    }
    // 统一扣血方法
    public void TakeDamage(float damage)
    {
        if (isUnbeatable == 1) return;
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        HealthBar healthBar = GetComponent<HealthBar>();
        if (healthBar != null)
        {
            healthBar.NotifyDamageTaken();
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 死亡逻辑
    void Die()
    {
        Debug.Log("已被摧毁");
        GameObject fx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 播放一次
        fx.GetComponent<ExplosionFX>().PlayOnce();

        // 爆炸自动销毁（防止残留）
        Destroy(fx, 2f);
        if (gameObject.CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<GameManage>().EndWork(-1);//GameLevelManager
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Debug.Log(enemies.Length);
            if (enemies.Length == 1)  // 只有自己
            {
                GameObject.Find("GameManager").GetComponent<GameManage>().EndWork(1);
            }
        }
        Destroy(gameObject);
    }
}
