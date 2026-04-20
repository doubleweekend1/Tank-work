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
    [Header("固定视角参数")]
    public Vector3 fixedset = new Vector3(0, 15, -5);
    private CameraState cameraState;
    void Start()
    {
        cameraState = GameObject.Find("Canvas").GetComponent<CameraState>();
    }

    void LateUpdate()
    {
        if (cameraState == null|| cameraState.Camerastate == 1)
        {
                if (target == null) return;
                Vector3 desiredPosition = target.TransformPoint(offset);
                //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = desiredPosition;
                transform.LookAt(target);
        }
        else
        {
            transform.position = fixedset;
            Vector3 belowPoint = transform.position + Vector3.down * 10f;
            transform.LookAt(belowPoint);
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
