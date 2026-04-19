using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ACManager : MonoBehaviour
{
    //public string Maxlevel = "maxlevel";
    public int Maxlevel;
    public int ShortestTime;
    public int FirstDie;
    public void Start()
    {
        Maxlevel=  PlayerPrefs.GetInt("Maxlevel", 1);
        ShortestTime = PlayerPrefs.GetInt("ShortestTime", 1000000000);
        FirstDie = PlayerPrefs.GetInt("FirstDie", 1);
    }
    public void save()
    {
        PlayerPrefs.SetInt("Maxlevel", Maxlevel);
        PlayerPrefs.SetInt("ShortestTime", ShortestTime);
        PlayerPrefs.SetInt("FirstDie", FirstDie);
    }
    public void updateAC()
    {
        GameObject.Find("ACmaxlevel").GetComponent<ScoreDisplay>().SetScore(Maxlevel);
        GameObject.Find("ACshortestTime").GetComponent<ScoreDisplay>().SetScore(ShortestTime);
    }
}
