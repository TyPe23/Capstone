using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortScript : MonoBehaviour
{
    // material colors  
    public Material Green;
    public Material Red;
    public Material Yellow;
    public Material Gray;

    // the place rendering 
    public Renderer Rightrend;
    public Renderer Leftrend;

    // broken funtions with default 
    public bool IsBroken = false;
    private int timer = 21;
    private int cur = 0;

    private float nextActionTime = 0.0f; 
    private float period = 1.0f; // how long in seconds to wait 

    // Start is called before the first frame update
    void Start()
    {
        // set defaults to green 
        Rightrend.enabled = true;
        Rightrend.sharedMaterial = Green;
        // set default to yellow 
        Leftrend.enabled = true;
        Leftrend.sharedMaterial = Yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBroken & Time.time > nextActionTime)
        {
            Rightrend.sharedMaterial = Yellow;
            nextActionTime += period;
            changeCollorLoop();
        }

    }

    void changeCollorLoop()
    {
        if (cur < timer)
        {
            if(cur%2 == 0) // even
            {
                //Debug.Log("EVEN");
                Leftrend.sharedMaterial = Gray;
            }
            else // odd 
            {
                //Debug.Log("ODD");
                Leftrend.sharedMaterial = Red;
            }
            cur++;
        }else if (cur >= timer)
        {
            //Debug.Log("RESET");
            cur = 0;
        }

    }

    // command to run if fixed 
    public void allFixed()
    {
        Rightrend.sharedMaterial = Green;
        Leftrend.sharedMaterial = Yellow;
        IsBroken = false;
    }
    // command to run if broken
    public void broken()
    {
        Rightrend.sharedMaterial = Yellow;
        Leftrend.sharedMaterial = Red;
        IsBroken = true;
    }

}
