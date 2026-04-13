using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flexible_wall : MonoBehaviour
{
    [Header("下沉深度")]
    public float hideDistance = 5f;

    [Header("移动速度")]
    public float moveSpeed = 4f;

    [Header("恢复等待时间(秒)")]
    public float recoverDelay = 5f;

    private Vector3 originalPos;
    private Vector3 hiddenPos;
    private bool isActive = true;

    void Start()
    {
        originalPos = transform.position;
        hiddenPos = originalPos + Vector3.down * hideDistance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && isActive)
        {
            isActive = false;
            StartCoroutine(HideAndRecover());
        }
    }

    IEnumerator HideAndRecover()
    {
        // 平滑下沉
        while (transform.position.y > hiddenPos.y + 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                hiddenPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // 关闭碰撞
        GetComponent<Collider>().enabled = false;

        // 等待恢复
        yield return new WaitForSeconds(recoverDelay);

        // 打开碰撞并升起
        GetComponent<Collider>().enabled = true;

        while (transform.position.y < originalPos.y - 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                originalPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        isActive = true;
    }
}
