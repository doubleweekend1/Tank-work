using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueCanvas : MonoBehaviour
{
    private static GameObject _originalCanvas;
    private static float _creationTime;

    void Awake()
    {
        // 记录当前对象的创建时间
        float currentTime = Time.realtimeSinceStartup;

        // 如果没有记录过原始Canvas，当前就是第一个
        if (_originalCanvas == null)
        {
            _originalCanvas = gameObject;
            _creationTime = currentTime;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"保留原始Canvas: {gameObject.name}, 创建时间: {currentTime}");
        }
        // 如果已经有原始Canvas，判断哪个是旧的
        else if (_originalCanvas != gameObject)
        {
            // 如果当前对象创建时间更晚，说明是新的，销毁
            if (currentTime > _creationTime)
            {
                Debug.Log($"销毁新Canvas: {gameObject.name} (创建时间: {currentTime} > {_creationTime})");
                Destroy(gameObject);
            }
            else
            {
                // 理论上不会走到这里，但为了安全
                Debug.Log($"异常情况：当前对象时间更早，保留当前，销毁原来的");
                Destroy(_originalCanvas);
                _originalCanvas = gameObject;
                _creationTime = currentTime;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
