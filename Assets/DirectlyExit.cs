using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectlyExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void DirectExit()
    {
        //GameObject.Find("AC2").GetComponent<ScoreDisplay>().AddScore(1);//GameLevelManager
        SceneManager.LoadScene("SceneUI");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
