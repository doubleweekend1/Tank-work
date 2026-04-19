using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI面板")]
    public GameObject startPanel;      // 开始界面
    public GameObject levelPanel;      // 选关界面
    public GameObject achievementPanel;// 成就界面
    public GameObject ingamePanel;
    public GameObject EndPanel;
    public GameObject PausePanel;
    public GameObject SetPanel;
    public int lasttime;
    public int currentlevel;
    void Awake()
    {
        // 让这个UI物体在场景切换时不被销毁
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        ShowStartPanel();
    }
    public void ShowAchievementPanel()
    {
        // 隐藏所有面板
        startPanel.SetActive(false);
        levelPanel.SetActive(false);
        ingamePanel.SetActive(false);
        EndPanel.SetActive(false);
        PausePanel.SetActive(false);
        SetPanel.SetActive(false);
    // 显示成就面板
    achievementPanel.SetActive(true);
        GameObject.Find("ACManager").GetComponent<ACManager>().updateAC();
        
        
    }
    public void ShowInGamePanel()
    {
        startPanel.SetActive(false);
        levelPanel.SetActive(false);
        achievementPanel.SetActive(false);
        EndPanel.SetActive(false);
        SetPanel.SetActive(false);
        
        ingamePanel.SetActive(true);
    }
    // 返回开始界面
    public void ShowStartPanel()
    {
        // 隐藏所有面板
        SetPanel.SetActive(false);
        achievementPanel.SetActive(false);
        levelPanel.SetActive(false);
        ingamePanel.SetActive(false);
        PausePanel.SetActive(false);
        EndPanel.SetActive(false);
        // 显示开始面板
        startPanel.SetActive(true);
        
    }

    // 显示选关界面
    public void ShowLevelPanel()
    {
        SetPanel.SetActive(false);
        startPanel.SetActive(false);
        achievementPanel.SetActive(false);
        ingamePanel.SetActive(false);
        levelPanel.SetActive(true);
        EndPanel.SetActive(false);
    }
    public void ShowEndPanel()
    {
        SetPanel.SetActive(false);
        startPanel.SetActive(false);
        achievementPanel.SetActive(false);
        ingamePanel.SetActive(false);
        levelPanel.SetActive(false);
        ////


        EndPanel.SetActive(true);
        GameObject.Find("ShowGameTime").GetComponent<ScoreDisplay>().SetScore(lasttime);
    }
    public void ShowSetPanel()
    {
        
        startPanel.SetActive(false);
        achievementPanel.SetActive(false);
        ingamePanel.SetActive(false);
        levelPanel.SetActive(false);
        EndPanel.SetActive(false);

        SetPanel.SetActive(true);
    }
}