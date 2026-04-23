using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class flexible_wall : MonoBehaviour
{
    [Header("下沉深度")]
    public float hideDistance = 5f;

    [Header("移动速度")]
    public float moveSpeed = 4f;

    [Header("恢复等待时间(秒)")]
    public float recoverDelay = 5f;

    public float detectsizex = 5;
    public float detectsizez = 5;
    public float detectdx;
    public float detectdz;
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
    public void pubHide()
    {
        if (isActive)
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



        // 等待恢复（重复检查直到上方为空）
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            if (!IsObjectAbove())
            {
                break; // 上方无物体，可以恢复
            }
            // 有物体则继续等待，重新开始5秒倒计时
        }
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
    /*private bool IsObjectAbove()
    {
        // 获取墙的顶部位置
        BoxCollider wallCollider = GetComponent<BoxCollider>();
        Vector3 checkPosition = transform.position + Vector3.up * (wallCollider.size.y / 2 + 0.1f);

        // 发射射线或检测重叠
        Collider[] hitColliders = Physics.OverlapBox(
            checkPosition,
            new Vector3(wallCollider.size.x / 2, 0.1f, wallCollider.size.z / 2),
            Quaternion.identity
        );

        foreach (var hit in hitColliders)
        {
            if (hit.gameObject.CompareTag("Player") || hit.gameObject.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }*/
    private bool IsObjectAbove()
    {
        BoxCollider wallCollider = GetComponent<BoxCollider>();

        // 扩大检测范围（例如：宽度和深度各扩大0.5倍）
        Vector3 checkSize = new Vector3(
            wallCollider.size.x * detectsizex,  // 
            5,                         // 厚度
            wallCollider.size.z * detectsizez   // 
        );

        //Vector3 checkPosition = transform.position + Vector3.up * (wallCollider.size.y / 2);
        Vector3 colliderCenter = transform.position + wallCollider.center;
        Vector3 deltaV = new Vector3(detectdx, 0, detectdz);
        Vector3 checkPosition = colliderCenter + Vector3.up * (wallCollider.size.y / 2)+deltaV;

        Collider[] hitColliders = Physics.OverlapBox(checkPosition, checkSize / 2, Quaternion.identity);

        foreach (var hit in hitColliders)
        {
            if (hit.gameObject.CompareTag("Player") || hit.gameObject.CompareTag("Enemy"))
            {
                return true;
            }
        }
        return false;
    }
    void OnDrawGizmos()
    {
        BoxCollider wallCollider = GetComponent<BoxCollider>();
        if (wallCollider == null) return;

        Vector3 checkSize = new Vector3(
            wallCollider.size.x * detectsizex,
            5,
            wallCollider.size.z * detectsizez
        );

        // Vector3 checkPosition = transform.position + Vector3.up * (wallCollider.size.y / 2);
        Vector3 colliderCenter = transform.position + wallCollider.center;
        Vector3 deltaV = new Vector3(detectdx, 0, detectdz);
        Vector3 checkPosition = colliderCenter + Vector3.up * (wallCollider.size.y / 2) + deltaV;
        // 半透明红色立方体显示检测区域
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(checkPosition, checkSize);

        // 黄色线框
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(checkPosition, checkSize);

        // 中心点标记
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(checkPosition, 0.1f);
    }
}
