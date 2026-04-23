using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    private UIManager UIM;
    private GamaLevelManager GLM;
    // Start is called before the first frame update
    void Start()
    {
        UIM = gameObject.GetComponent<UIManager>();
        GLM = gameObject.GetComponent<GamaLevelManager>();
    }
    public void replay()
    {
            GLM.LoadSceneByIndex(UIM.currentlevel);
    }
}
