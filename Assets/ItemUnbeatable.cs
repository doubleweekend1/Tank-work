using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnbeatable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))//"Enemy"
        {
            HP hp = collision.collider.GetComponent<HP>();
            if (hp != null)
            {
                hp.BecomeUnbeatable();
            }
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
