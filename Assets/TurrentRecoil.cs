using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRecoil : MonoBehaviour
{
    [Header("炮台")]
    public Transform turret; // 你的炮台

    [Header("后坐力参数")]
    public float recoilDistance = 0.12f; // 向后缩的距离
    public float recoilSpeed = 20f;      // 回弹速度

    private Vector3 _turretDefaultPos;

    void Start()
    {
        _turretDefaultPos = turret.localPosition;
    }

    void Update()
    {
        // 平滑回弹
        turret.localPosition = Vector3.Lerp(
            turret.localPosition,
            _turretDefaultPos,
            recoilSpeed * Time.deltaTime
        );
    }

    // 你发射子弹时调用这个方法
    public void DoRecoil()
    {
        turret.localPosition -= turret.forward * recoilDistance;
    }
}