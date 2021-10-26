using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bool easyMode = true;
    void Awake()
    {
        Time.timeScale = 0;
    }

    public void StartGameEasy()
    {
        Time.timeScale = 1;
    }
    
    public void StartGameHard()
    {
        Time.timeScale = 1;
        easyMode = false;
    }
    
}
