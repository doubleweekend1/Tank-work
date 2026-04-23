using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Text))]
public class ResultText : MonoBehaviour
{
    private TextMeshProUGUI resultText;
    void Awake()
    {
        // 自动获取自身的 Text 组件
        resultText = GetComponent<TextMeshProUGUI>();
        // 初始隐藏
    }

    /// <summary>
    /// 外部调用：显示胜利
    /// </summary>
    public void ShowVictory()
    {
        resultText.text = "Win";
        resultText.color = Color.green;
        resultText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 外部调用：显示失败
    /// </summary>
    public void ShowDefeat()
    {
        resultText.text = "Defeated";
        resultText.color = Color.red;
        resultText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏文本
    /// </summary>
    public void Hide()
    {
        resultText.gameObject.SetActive(false);
    }
}