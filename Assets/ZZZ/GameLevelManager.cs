using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaLevelManager : MonoBehaviour
{
    private UIManager UIM;
    private ScoreDisplay LOT;
    // Start is called before the first frame update
    public int Opened_level = 1;
    public int TotalLevel = 4;
    void Start()
    {
        
        Opened_level = PlayerPrefs.GetInt("UnlockedLevel", 1);
        UIM=GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    // 方法1：通过场景名称跳转
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        /////////////////GameObject.Find("GameManager").GetComponent<GameManage>().BeginWork(1);
        UIM.currentlevel = 1;
        UIM.ShowInGamePanel();
    }

    // 方法2：通过场景索引跳转（数字）
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex <= Opened_level&&sceneIndex<=TotalLevel)
        {
            
            SceneManager.LoadScene(sceneIndex);
            UIM.currentlevel = sceneIndex;
            UIM.ShowInGamePanel();
        }
        
    }
    // Update is called once per frame
    public void unlockLevel(int x)
    {
        if(Opened_level < x)
        {
             Opened_level = Mathf.Min(x,TotalLevel);
        }
        
    }
    public void save()
    {
        PlayerPrefs.SetInt("UnlockedLevel", Opened_level);
    }
}
