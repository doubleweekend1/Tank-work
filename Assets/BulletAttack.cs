using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ///////////////
        Debug.Log("撞到了：" + collision.collider.name + " 标签：" + collision.collider.tag);

        if (collision.collider.CompareTag("Untagged"))
        {
            Debug.Log("成功识别untagged！");
        }
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("成功识别敌方坦克！");
        }
        //////////////
        // 碰到敌方坦克，不产生物理效果
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("attackhappen");
            // 禁用这次碰撞的物理效果
            //rb.isKinematic = true;
            //rb.velocity = Vector3.zero;
            HP hp = collision.collider.GetComponent<HP>();
            if (hp != null)
            {
                hp.TakeDamage(5);
            }
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
