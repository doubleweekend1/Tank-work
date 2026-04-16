using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWeapons : MonoBehaviour
{
    // Start is called before the first frame update
    public FireType ft;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("Player"))//"Enemy"
        {

            FireManager FM = collision.collider.GetComponent<FireManager>();
            if (FM != null)
            {
                FM.SwitchFireType(ft);
            }
            

            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
