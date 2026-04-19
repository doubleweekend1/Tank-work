using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFire2 : MonoBehaviour
{
    [Header("发射点2")]
    public Transform firePoint2;
    [Header("发射点3")]
    public Transform firePoint3;

    private Transform firePoint0;
    public TankFire1 tankFire1;
    public float fireInterval = 0.3f;
    public void Fire()
    {
        doublefire();
        
    }
    private void doublefire()
    {
        if (tankFire1 != null)
        {
            firePoint0 = tankFire1.firePoint;
            tankFire1.firePoint = firePoint2;
            tankFire1.Fire();
            tankFire1.firePoint = firePoint3;
            tankFire1.Fire();
            tankFire1.firePoint= firePoint0 ;
        }
    }
}
