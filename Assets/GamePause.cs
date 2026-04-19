using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    // 用于存储暂停菜单面板的引用（可选，但推荐）
    public GameObject pauseMenuPanel;
    public GameObject ingamePanel;
    private bool isPaused = false;

    void Update()
    {
        // 可选功能：按键盘上的 Escape 键也能暂停/恢复游戏
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // 这个方法会通过按钮的 OnClick 事件调用
    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    // 暂停游戏
    public void PauseGame()
    {
        Time.timeScale = 0f; // 将时间流速设为0，游戏逻辑暂停
        isPaused = true;

        // 如果有暂停菜单面板，则显示它
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(true);
        if (ingamePanel != null)
            ingamePanel.SetActive(false);
        Debug.Log("游戏已暂停");
    }

    // 恢复游戏
    public void ResumeGame()
    {
        Time.timeScale = 1f; // 恢复时间流速
        isPaused = false;
        if (ingamePanel != null)
            ingamePanel.SetActive(true);
        // 如果有暂停菜单面板，则隐藏它
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
        
        Debug.Log("游戏已恢复");
    }
}