using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to check if 2 ports have been connected/fixed and stay that way 

public class PortChecker : MonoBehaviour
{
    // connect the two ports through their scripts 
    public PortScript Starting; 
    public PortScript Ending;

    // task 
    public TaskList UI;
    public int taskNum = 2;

    public bool allDone = false;

    // Called when the script is loaded for thr first time
    void Awake() {
        // used for the Level 3 Terminal
        PlayerPrefs.SetString("cableHookedUp", "false");
    }

    // Update is called once per frame
    void Update()
    {
        // if both are fixed 
        if (Starting.IsBroken == false && Ending.IsBroken == false)
        {
            if (allDone == false)
            {
                allDone = true;
                PlayerPrefs.SetString("cableHookedUp", "true");
                UI.taskDone(taskNum);
            }
        }
        else // if one is broken 
        {
            if (allDone == true) // but both were fixed at one time 
            {
                allDone = false;
                PlayerPrefs.SetString("cableHookedUp", "false");
            }
        }
    }
}
