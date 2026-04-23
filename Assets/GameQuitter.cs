using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    // 这个方法会在按钮被点击时调用
    public void QuitGame()
    {
        // 退出游戏
        Application.Quit();

        // 注意：在Unity编辑器中测试时，Application.Quit() 不会生效。
        // 如果你希望在编辑器里点击按钮也能停止运行，可以取消下面这行的注释
         #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
         #endif
    }
}