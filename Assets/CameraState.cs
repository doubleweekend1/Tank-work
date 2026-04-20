using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraState : MonoBehaviour
{
    public int Camerastate=0;
    public ScoreDisplay SD;
    // Start is called before the first frame update
    void Start()
    {
        //SD=GameObject.Find("CameraFollowText").GetComponent<ScoreDisplay>();
    }
    public void change()
    {
        Camerastate = 1 - Camerastate;
        SD.SetScore(Camerastate);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
