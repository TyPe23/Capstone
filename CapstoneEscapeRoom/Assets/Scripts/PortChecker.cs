using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to check if 2 ports have been connected/fixed and stay that way 

public class PortChecker : MonoBehaviour
{
    // connect the two ports through there scripts 
    public PortScript Starting; 
    public PortScript Ending;
    
    public bool allDone = false;

    // Update is called once per frame
    void Update()
    {
        // if both are fixed 
        if (Starting.IsBroken == false && Ending.IsBroken == false && allDone == false)
        {
            allDone = true;
        }
        else // if one is broken 
        {
            if (allDone == true) // but both were fixed at one time 
            {
                allDone = false;
            }
        }
    }
}
