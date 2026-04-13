using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire2 : MonoBehaviour
{
    public TankFire1 tankFire1;
    public float fireInterval = 0.3f;
    public void Fire()
    {
        StartCoroutine(doublefire());
        
    }
    IEnumerator doublefire()
    {
        if (tankFire1 != null)
        {
            tankFire1.Fire();
            yield return new WaitForSeconds(fireInterval);
            tankFire1.Fire();
        }
    }
}
