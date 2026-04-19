using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZZZZ___ontanktestwave : MonoBehaviour
{
    public GameObject sonicWavePrefab;  // 声波预制体（带SpriteRenderer）
    public float shootCooldown = 1f;
    public float shootSpeed = 10f;

    private float nextShootTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextShootTime)
        {
            ShootSonicWave();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    void ShootSonicWave()
    {
        // 在坦克位置生成声波
        GameObject wave = Instantiate(sonicWavePrefab, transform.position, Quaternion.Euler(0, 0, 0));//Quaternion.identity

        // 设置移动参数
        Wave projectile = wave.GetComponent<Wave>();
        if (projectile != null)
        {
            projectile.speed = shootSpeed;
        }
    }
}
