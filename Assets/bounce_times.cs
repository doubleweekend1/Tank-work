using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce_times : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("最大反弹次数")]
    public int maxBounceCount = 3;
    private int currentBounceCount = 0;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 碰到东西 = 反弹一次
        currentBounceCount++;

        // 超过最大次数 → 销毁子弹
        if (currentBounceCount >= maxBounceCount)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
