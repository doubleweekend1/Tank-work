using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Exit()
    {
        GameObject.Find("Canvas").GetComponent<GamaLevelManager>().save();
        GameObject.Find("ACmanager").GetComponent<ACManager>().save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
