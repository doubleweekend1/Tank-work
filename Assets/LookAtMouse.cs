using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    void Update()
    {
        // 射线从摄像机穿过鼠标位置打到地面层
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // 获取鼠标命中的世界点
            Vector3 targetPos = hit.point;
            // 保持Y不变，只水平旋转（防止坦克低头仰头）
            targetPos.y = transform.position.y;
            // 朝向目标
            transform.LookAt(targetPos);
        }
    }
}