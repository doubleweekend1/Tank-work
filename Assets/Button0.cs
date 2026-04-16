using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button0 : MonoBehaviour
{
    // 方法1：通过场景名称跳转
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
        
    // 方法2：通过场景索引跳转（数字）
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ZLoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}