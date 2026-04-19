using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Explosion : MonoBehaviour
{
    [Header("音效")]
    public AudioClip explosionSound;  // 拖拽你的音效文件到这里

    [Header("特效预制体")]
    public GameObject explosionParticlePrefab;  // 拖拽你的粒子特效预制体
    public GameObject smokePrefab;              // 拖拽你的烟雾预制体
    public GameObject firePrefab;               // 拖拽你的火焰预制体

    [Header("特效参数")]
    public float explosionRadius = 3f;
    public float lightIntensity = 5f;
    public float lightDuration = 0.3f;

    public void Explode()
    {
        // 1. 播放音效（修正：添加了这行）
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1f);
        }

        // 2. 实例化粒子特效（使用你的美术资源）
        if (explosionParticlePrefab != null)
        {
            GameObject particles = Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
            Destroy(particles, 2f);
        }

        // 3. 实例化烟雾（使用你的美术资源）
        if (smokePrefab != null)
        {
            GameObject smoke = Instantiate(smokePrefab, transform.position, Quaternion.identity);
            Destroy(smoke, 3f);
        }

        // 4. 实例化火焰（使用你的美术资源）
        if (firePrefab != null)
        {
            GameObject fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            Destroy(fire, 2f);
        }

        // 5. 添加临时光源（增强效果）
        StartCoroutine(CreateFlashLight());

        // 6. 镜头震动
        StartCoroutine(CameraShake());

        // 7. 销毁坦克
        Destroy(gameObject, 0.1f);
    }

    IEnumerator CreateFlashLight()
    {
        GameObject lightObj = new GameObject("ExplosionLight");
        lightObj.transform.position = transform.position;
        Light flashLight = lightObj.AddComponent<Light>();
        flashLight.color = new Color(1f, 0.5f, 0f);
        flashLight.intensity = lightIntensity;
        flashLight.range = explosionRadius * 3f;

        float elapsed = 0f;
        while (elapsed < lightDuration)
        {
            elapsed += Time.deltaTime;
            flashLight.intensity = Mathf.Lerp(lightIntensity, 0, elapsed / lightDuration);
            yield return null;
        }

        Destroy(lightObj);
    }

    IEnumerator CameraShake()
    {
        Camera cam = Camera.main;
        if (cam == null) yield break;

        Vector3 originalPos = cam.transform.position;
        float elapsed = 0f;
        float shakeDuration = 0.3f;
        float shakeMagnitude = 0.3f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            cam.transform.position = originalPos + new Vector3(x, y, 0);
            yield return null;
        }

        cam.transform.position = originalPos;
    }
}