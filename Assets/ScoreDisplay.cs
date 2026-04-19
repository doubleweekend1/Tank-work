using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // 必须引入TextMeshPro命名空间

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // 拖拽赋值
    public string ACname; 
    private int score = 0;

    void Start()
    {
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = ACname + score;
    }

    public void SetScore(int points)
    {
        score = points;
        UpdateScoreDisplay();
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }
}
