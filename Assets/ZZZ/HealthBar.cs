using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("血条设置")]
    public bool useExistingCanvas = false; // 是否使用场景中已有的Canvas
    public Canvas targetCanvas; // 如果useExistingCanvas为true，指定目标Canvas
    public Vector3 offset = new Vector3(0, 1.5f, 0);
    public float hideDelay = 3f;
    public float smoothSpeed = 5f;
    public Vector2 barSize = new Vector2(2f, 0.3f);

    [Header("颜色设置")]
    public Color fullHealthColor = Color.green;
    public Color lowHealthColor = Color.red;

    private HP hpScript;
    private Canvas healthBarCanvas;
    private Image healthBarImage;
    private RectTransform canvasRect;
    private float lastDamageTime;
    private bool isVisible = true;
    private float targetFillAmount;
    private Camera mainCamera;
    public GameObject barObj;
    void Start()
    {
        hpScript = GetComponent<HP>();
        if (hpScript == null)
        {
            Debug.LogError("HP script not found on " + gameObject.name);
            return;
        }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
            return;
        }

        CreateHealthBar();

        lastDamageTime = -hideDelay;
        targetFillAmount = hpScript.currentHealth / hpScript.MaxHealth;
        UpdateHealthBarColor();
        ShowHealthBar();
    }

    void CreateHealthBar()
    {
        if (useExistingCanvas && targetCanvas != null)
        {
            // 使用场景中已有的Canvas（Screen Space模式）
            CreateWorldSpaceBarOnExistingCanvas();
        }
        else
        {
            // 创建新的World Space Canvas
            CreateWorldSpaceBar();
        }
    }

    void CreateWorldSpaceBar()
    {
        // 创建独立的World Space Canvas
        GameObject canvasObj = new GameObject("HealthBarCanvas");
        canvasObj.transform.SetParent(transform);
        healthBarCanvas = canvasObj.AddComponent<Canvas>();
        healthBarCanvas.renderMode = RenderMode.WorldSpace;
        healthBarCanvas.sortingOrder = -100;  // ← 只加这一行
        // 设置Canvas不阻挡射线
        CanvasGroup canvasGroup = canvasObj.AddComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // 设置Canvas大小
        canvasRect = canvasObj.GetComponent<RectTransform>();
        canvasRect.sizeDelta = barSize;

        // 创建血条UI元素
        CreateHealthBarUI(canvasObj);
    }

    void CreateWorldSpaceBarOnExistingCanvas()
    {
        // 在已有Canvas下创建血条（作为独立的UI元素）
        GameObject barContainer = new GameObject("HealthBar_" + gameObject.name);
        barContainer.transform.SetParent(targetCanvas.transform);

        // 添加RectTransform
        RectTransform containerRect = barContainer.AddComponent<RectTransform>();
        containerRect.sizeDelta = barSize;

        // 添加CanvasGroup确保不阻挡射线
        CanvasGroup canvasGroup = barContainer.AddComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // 创建血条UI元素
        CreateHealthBarUI(barContainer);

        // 注意：使用已有Canvas时，血条不会跟随角色移动，需要额外处理
        Debug.LogWarning("Using existing Canvas, health bar will not follow the character automatically!");
    }

    void CreateHealthBarUI(GameObject parent)
    {
        // 创建背景
        GameObject backgroundObj = new GameObject("Background");
        backgroundObj.transform.SetParent(parent.transform);
        /*RectTransform bgRect = backgroundObj.AddComponent<RectTransform>();
        bgRect.anchorMin = new Vector2(0, 0.5f);
        bgRect.anchorMax = new Vector2(1, 0.5f);
        bgRect.sizeDelta = new Vector2(0, barSize.y * 0.7f);*/



        RectTransform bgRect = backgroundObj.AddComponent<RectTransform>();
        bgRect.anchorMin = new Vector2(0, 0);
        bgRect.anchorMax = new Vector2(1, 1);
        bgRect.sizeDelta = Vector2.zero;



        bgRect.anchoredPosition = Vector2.zero;


        Image bgImage = backgroundObj.AddComponent<Image>();
        bgImage.color = new Color(0, 0, 0, 0.7f);

        // 创建血条前景
        barObj = new GameObject("HealthBar");
        barObj.transform.SetParent(parent.transform);
        /*RectTransform barRect = barObj.AddComponent<RectTransform>();
        barRect.anchorMin = new Vector2(0, 0.5f);
        barRect.anchorMax = new Vector2(1, 0.5f);
        barRect.sizeDelta = new Vector2(0, barSize.y * 0.7f);
        barRect.anchoredPosition = Vector2.zero;*/


        RectTransform barRect = barObj.AddComponent<RectTransform>();
        // 改为拉伸模式
        barRect.anchorMin = new Vector2(0, 0);
        barRect.anchorMax = new Vector2(1, 1);
        barRect.sizeDelta = Vector2.zero;
        barRect.anchoredPosition = Vector2.zero;
        /*RectTransform barRect = barObj.AddComponent<RectTransform>();
        // 关键：不拉伸，用固定尺寸，fillAmount才能生效
        barRect.anchorMin = new Vector2(0, 0.5f);
        barRect.anchorMax = new Vector2(0, 0.5f);
        barRect.sizeDelta = new Vector2(barSize.x, barSize.y * 0.8f);
        barRect.pivot = new Vector2(0, 0.5f); // 从左侧填充
        barRect.anchoredPosition = Vector2.zero;*/




        healthBarImage = barObj.AddComponent<Image>();
        healthBarImage.type = Image.Type.Filled;
        healthBarImage.fillMethod = Image.FillMethod.Horizontal;
        //healthBarImage.fillOrigin = 0;
        healthBarImage.fillOrigin = (int)Image.OriginHorizontal.Left;
        healthBarImage.color = fullHealthColor;
        // 创建边框
        /*GameObject borderObj = new GameObject("Border");
        borderObj.transform.SetParent(parent.transform);
        RectTransform borderRect = borderObj.AddComponent<RectTransform>();
        borderRect.anchorMin = Vector2.zero;
        borderRect.anchorMax = Vector2.one;
        borderRect.sizeDelta = Vector2.zero;

        Image borderImage = borderObj.AddComponent<Image>();
        borderImage.color = Color.white;
        borderImage.raycastTarget = false;*/

        GameObject borderObj = new GameObject("Border");
        borderObj.transform.SetParent(parent.transform);
        RectTransform borderRect = borderObj.AddComponent<RectTransform>();
        borderRect.anchorMin = Vector2.zero;
        borderRect.anchorMax = Vector2.one;
        borderRect.sizeDelta = Vector2.zero;

        Image borderImage = borderObj.AddComponent<Image>();
        borderImage.color = new Color(1, 1, 1, 0); // 完全透明
        borderImage.raycastTarget = false;

        // 添加轮廓效果
        Outline outline = borderObj.AddComponent<Outline>();
        outline.effectColor = Color.white;
        outline.effectDistance = new Vector2(2, 2);
        // 保存Canvas引用
        if (healthBarCanvas == null && parent.GetComponent<Canvas>() != null)
        {
            healthBarCanvas = parent.GetComponent<Canvas>();
        }
    }

    void Update()
    {
        if (hpScript == null || healthBarImage == null) return;

        // 更新血量显示
        float currentFill = hpScript.currentHealth / hpScript.MaxHealth; Debug.Log($"血量百分比: {currentFill}, fillAmount: {healthBarImage.fillAmount}");
        targetFillAmount = currentFill;
        healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, targetFillAmount, Time.deltaTime * smoothSpeed);
        barObj.transform.localScale = new Vector3(currentFill, 1, 1);//////////////////////
        barObj.transform.localPosition = new Vector3((currentFill-1)/2, 0, 0);
        // 假设原始宽度为1，原始位置在中心
        // 获取缩放前左端点的世界位置
        // 更新颜色
        UpdateHealthBarColor();

        // 检查隐藏
        if (Time.time - lastDamageTime > hideDelay && isVisible)
        {
            HideHealthBar();
        }

        // 如果是独立的World Space Canvas，让血条面向相机
        if (!useExistingCanvas && healthBarCanvas != null && healthBarCanvas.renderMode == RenderMode.WorldSpace)
        {
            healthBarCanvas.transform.LookAt(mainCamera.transform);
            healthBarCanvas.transform.Rotate(0, 180, 0);
        }

        // 更新位置（仅对独立Canvas有效）
        if (!useExistingCanvas && healthBarCanvas != null)
        {
            healthBarCanvas.transform.position = transform.position + offset;
        }
    }

    void UpdateHealthBarColor()
    {
        if (healthBarImage == null) return;

        float healthPercent = healthBarImage.fillAmount;
        healthBarImage.color = Color.Lerp(lowHealthColor, fullHealthColor, healthPercent);
    }

    void ShowHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.enabled = true;
            // 同时显示背景和边框
            Transform parent = healthBarImage.transform.parent;
            if (parent != null)
            {
                Transform bg = parent.Find("Background");
                if (bg != null) bg.GetComponent<Image>().enabled = true;

                Transform border = parent.Find("Border");
                if (border != null) border.GetComponent<Image>().enabled = true;
            }
            isVisible = true;
        }
    }

    void HideHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.enabled = false;
            // 同时隐藏背景和边框
            Transform parent = healthBarImage.transform.parent;
            if (parent != null)
            {
                Transform bg = parent.Find("Background");
                if (bg != null) bg.GetComponent<Image>().enabled = false;

                Transform border = parent.Find("Border");
                if (border != null) border.GetComponent<Image>().enabled = false;
            }
            isVisible = false;
        }
    }

    void OnDamageTaken()
    {
        lastDamageTime = Time.time;
        if (!isVisible)
        {
            ShowHealthBar();
            if (healthBarImage != null)
            {
                targetFillAmount = hpScript.currentHealth / hpScript.MaxHealth;
                healthBarImage.fillAmount = targetFillAmount;
            }
        }
    }
    public void NotifyDamageTaken()
    {
        OnDamageTaken();
    }
}