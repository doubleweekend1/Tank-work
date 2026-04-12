using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("跟随目标")]
    public Transform target; // 这里拖你的己方坦克

    [Header("跟随参数")]
    public Vector3 offset = new Vector3(0, 15, -5); // 相机相对坦克的偏移
    public float smoothSpeed = 5f; // 跟随平滑度

    void LateUpdate()
    {
        if (target == null) return;

        // 1. 计算目标位置：坦克位置 + 偏移
        Vector3 desiredPosition = target.TransformPoint(offset);

        // 2. 平滑插值：让相机移动更丝滑
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // 3. 赋值位置
        transform.position = desiredPosition;

        // 4. 始终看向坦克
        transform.LookAt(target);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
