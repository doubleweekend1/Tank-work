using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankFire2 : MonoBehaviour
{
    [Header("发射点2")]
    public Transform firePoint2;
    [Header("发射点3")]
    public Transform firePoint3;

    private Transform firePoint0;
    public EnemyTankFire1 EtankFire1;
    public float fireInterval = 0.3f;
    public void Fire()
    {
        doublefire();

    }
    private void doublefire()
    {
        if (EtankFire1 != null)
        {
            firePoint0 = EtankFire1.firePoint;
            EtankFire1.firePoint = firePoint2;
            EtankFire1.Fire();
            EtankFire1.firePoint = firePoint3;
            EtankFire1.Fire();
            EtankFire1.firePoint = firePoint0;
        }
    }
}
