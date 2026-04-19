using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddBounceTimes : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Tank;
    public float continuetimes;
    public int newtimes;
    private TankFire1 TF;
    public int oritimes;
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        TF = Tank.GetComponent<TankFire1>();
        oritimes = TF.bouncetimes;
        Debug.Log("oritiemsBBBBBBBBBBBBBBBBBBBBBBBBBB");
        Debug.Log(oritimes);
        
        if (collision.collider.CompareTag("Player"))//"Enemy"
        {
            ModifyBounceTimes();
            transform.Translate(Vector3.down * 1000);
        }
    }
    void ModifyBounceTimes()
    {
        
        if (TF != null)
        {
            TF.bouncetimes += newtimes;  // 修改预制体的times参数
            Invoke("changeback",continuetimes);
        }
    }
    void changeback()
    {
        TF.bouncetimes -= newtimes;
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        Debug.Log(TF.bouncetimes);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
