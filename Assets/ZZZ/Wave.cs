using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float speed = 8f;      // 移动速度
    public float lifetime = 1.5f; // 存在时间
    public AnimationCurve alphaCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);

    public string TargetTag;
    public float maxScale = 3f;  // 最大缩放倍数
    public bool useBoxCollider = true;  // 使用BoxCollider
    public float colliderWidth = 1f;    // 碰撞体宽度
    public float colliderHeight = 0.3f; // 碰撞体高度
    private Vector3 originalScale;  // 原始大小

    private SpriteRenderer spriteRenderer;
    private float timer = 0f;
    private Vector3 moveDirection;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 添加碰撞体
        if (useBoxCollider)
        {
            BoxCollider boxCol = gameObject.GetComponent<BoxCollider>();
            if (boxCol != null)
            {
                Debug.Log("碰撞体存在, IsTrigger: " + boxCol.isTrigger + ", Size: " + boxCol.size);
            }
            else
            {
                Debug.LogError("没有找到 BoxCollider！");
            }
            if (boxCol == null)
                boxCol = gameObject.AddComponent<BoxCollider>();

            boxCol.isTrigger = true;
            boxCol.size = new Vector3(colliderWidth, colliderHeight, 0.1f);
        }
        // 默认向前移动，你可以改成任意方向
        /*Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));
        mouseWorldPos.y = transform.position.y;  // 强制水平
        if(TargetTag=="Enemy")
            moveDirection = (mouseWorldPos - transform.position).normalized;
        else if (TargetTag == "palyer" || true)
        {
            moveDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        }

            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);*/

        Vector3 mouseScreenPos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 mouseWorldPos = hit.point;
            mouseWorldPos.y = transform.position.y;  // 强制水平

            if (TargetTag == "Enemy")
                moveDirection = (mouseWorldPos - transform.position).normalized;
            else if (TargetTag == "player" || true)
            {
                moveDirection = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
            }

            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }










        originalScale = transform.localScale;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifetime;

        // 向前移动
        transform.position += moveDirection * speed * Time.deltaTime;
        float scale = Mathf.Lerp(originalScale.x, maxScale, t);
        transform.localScale = new Vector3(scale, scale, 1);
        // 更新碰撞体大小
        if (useBoxCollider)
        {
            BoxCollider boxCol = GetComponent<BoxCollider>();
            if (boxCol != null)
            {
                Vector3 newSize = boxCol.size;
                newSize.x = colliderWidth * scale;
                boxCol.size = newSize;
            }
        }
        // 逐渐透明
        if (spriteRenderer != null)
        {
            Color c = spriteRenderer.color;
            c.a = alphaCurve.Evaluate(t);
            spriteRenderer.color = c;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // 在Unity里通过Layer或Tag过滤，不在代码里写死
        Debug.Log("声波接触到: " + other.name);
        Debug.Log("撞到了：" + other.name + " 标签：" + other.tag);
        if (other.CompareTag(TargetTag))//"Enemy"
        {
            Debug.Log("attackhappen");
            HP hp = other.GetComponent<HP>();
            if (hp != null)
            {
                hp.TakeDamage(15);
            }
            //Destroy(gameObject);

        }
    }
}
