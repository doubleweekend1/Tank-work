using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireType
{
    Fire1,
    Fire2,
    Fire3,
    Fire4
}

public class FireManager : MonoBehaviour
{
    [Header("当前开火模式")]
    public FireType currentFireType;

    // 引用你的4个开火脚本
    public TankFire1 tankFire1;
    public TankFire2 tankFire2;
    public TankFire3 tankFire3;
    public TankFire4 tankFire4;

    void Update()
    {
        // 鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("cleck mouse");
            FireByCurrentType();
            
        }
    }

    // 根据当前类型，自动选开火方式
    void FireByCurrentType()
    {
        switch (currentFireType)
        {
            case FireType.Fire1:
                tankFire1.Fire();
                break;
            case FireType.Fire2:
                tankFire2.Fire();
                break;
            case FireType.Fire3:
                tankFire3.Fire();
                break;
            case FireType.Fire4:
                tankFire4.Fire();
                break;
            
        }
    }

    // 外部调用：切换开火模式（比如按键1234）
    public void SwitchFireType(FireType type)
    {
        currentFireType = type;
    }
}