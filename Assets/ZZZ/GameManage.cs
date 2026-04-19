using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    private float startTime;      // 开始时间
    private float endTime;        // 结束时间
    private bool isTiming = false; // 是否正在计时
    public int level;
    // 开始计时
    private void Start()
    {
        BeginWork(GameObject.Find("Canvas").GetComponent<UIManager>().currentlevel);
    }
    public void BeginWork(int x)
    {
        startTime = Time.time;     // 记录当前时间
        isTiming = true;
        Debug.Log("开始计时：" + startTime);

        level = x;
    }

    // 结束计时，返回用时（秒）
    public float EndWork(int ifwin)
    {
        Debug.Log("ENDWORK");
        if (!isTiming)
        {
            Debug.LogWarning("还没有开始计时！");
            //return 0;
        }

        endTime = Time.time;
        float duration = endTime - startTime;
        isTiming = false;

        Debug.Log("结束计时，用时：" + duration + " 秒");
        if (ifwin == 1)
        {
            ACManager ACM = GameObject.Find("ACManager").GetComponent<ACManager>();
            UIManager UIM = GameObject.Find("Canvas").GetComponent<UIManager>();
            ACM.Maxlevel = Mathf.Max( level, ACM.Maxlevel);
            GameObject.Find("Canvas").GetComponent<GamaLevelManager>().unlockLevel(level+1);//GameLevelManager
            ACM.ShortestTime = Mathf.Min(ACM.ShortestTime, (int)duration);
            UIM.lasttime = (int)duration;
            Application.targetFrameRate = 60;
            SceneManager.LoadScene("SceneUI");
            UIM.ShowEndPanel();
        }
        if (ifwin == -1)
        {
            //GameObject.Find("AC2").GetComponent<ScoreDisplay>().SetScore(1);//GameLevelManager
            ACManager ACM = GameObject.Find("ACManager").GetComponent<ACManager>();
            UIManager UIM = GameObject.Find("Canvas").GetComponent<UIManager>();
            ACM.ShortestTime = Mathf.Min(ACM.ShortestTime, (int)duration);
            ACM.FirstDie = 1;
            UIM.lasttime = (int)duration;
            Application.targetFrameRate = 60;
            SceneManager.LoadScene("SceneUI");
            UIM.ShowEndPanel();

        }
        return duration;
    }
}
