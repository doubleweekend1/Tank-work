using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Tank;
    public float continuetimes;
    public float newV;
    private MyMove TV;
    public float oriV;
    private int cnt;
    private void Start()
    {
        cnt = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.collider.CompareTag("Player"))//"Enemy"
        {       
            TV = Tank.GetComponent<MyMove>();
            oriV = TV.moveSpeed;
            Debug.Log("oritiemsBBBBBBBBBBBBBBBBBBBBBBBBBB");
            Debug.Log(oriV);
            ModifyBounceTimes();
            transform.Translate(Vector3.down * 1000);
        }
    }
    void ModifyBounceTimes()
    {
        cnt++;
        if (TV != null)
        {
            TV.moveSpeed += newV;  // 修改预制体的times参数
            Invoke("changeback", continuetimes);
        }
    }
    void changeback()
    {
        TV.moveSpeed -= newV;
        Debug.Log("VCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC");

        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
