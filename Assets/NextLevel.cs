using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private UIManager UIM;
    private GamaLevelManager GLM;
    // Start is called before the first frame update
    void Start()
    {
        UIM = gameObject.GetComponent<UIManager>();
        GLM = gameObject.GetComponent<GamaLevelManager>();
    }
    public void trynextlevel()
    {
        if (UIM.currentlevel + 1 <= GLM.Opened_level)
        {
            GLM.LoadSceneByIndex(UIM.currentlevel + 1);
        }
        else
        {
            SceneManager.LoadScene("SceneUI");
            UIM.ShowStartPanel();
        }
    }
}
