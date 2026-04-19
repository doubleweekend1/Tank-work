using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFX : MonoBehaviour
{
    private ParticleSystem[] _ps;

    void Awake()
    {
        _ps = GetComponentsInChildren<ParticleSystem>();
    }

    // 外部调用这个方法，播放一次爆炸
    public void PlayOnce()
    {
        foreach (var p in _ps)
        {
            p.Stop();   // 先重置
            p.Play();   // 再播放
        }
    }
}

