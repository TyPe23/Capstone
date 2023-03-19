// Name: Matthew Tucker 
// Description: Timer function that can go up or down 
// Date: 1/31/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.XR.Interaction.Toolkit;
public class Timer : MonoBehaviour
{
    
    public TextMeshProUGUI timerText; // text location 
    public TextMeshProUGUI endTimerText; // text location 
    public GameObject Locamotion;
    public LocomotionSystem movement;
    public ActionBasedContinuousMoveProvider rotation;


    // timer min and max values for seconds, minutes, and hours  - if changed in unity will stay to that as a count down 
    [Header("Timer values")]
    [Range(0, 60)]
    public int seconds;
    [Range(0, 60)]
    public int minutes;
    [Range(0, 60)]
    public int hours;


    private float currentSeconds; // curent time 
    private int timerDefault; // default - starting 

    public int CurrentTime; 

    public bool CountDown = false;
    public bool Done = false; 
    // Start is called before the first frame update
    void Start()
    {
        timerDefault = 0; // set time start 
        timerDefault += (seconds + (minutes * 60))+(hours*60*60); // time to seconds 
        currentSeconds = timerDefault; // set current time 
    }

    // Update is called once per frame
    void Update()
    {
        if (!Done) // if not done with level 
        {
            if (CountDown) // count down 
            {
                if ((currentSeconds -= Time.deltaTime) <= 0) // time up 
                {
                    TimeUp();
                }
                else // update time 
                {
                    timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"hh\:mm\:ss"); // get time and formating 
                    CurrentTime = (int)currentSeconds; // show time in second as int
                }
            }
            else // count up 
            {
                currentSeconds += Time.deltaTime;
                timerText.text = TimeSpan.FromSeconds(currentSeconds).ToString(@"hh\:mm\:ss"); // get time and formating 
                CurrentTime = (int)currentSeconds; // show time in seconds as int 
            }
        }
    }

    // time is up (0 seconds left ) 
    private void TimeUp()
    {
        CurrentTime = 0;
        timerText.text = "00:00:00";
    }

    public void StopTimer()
    {
        Done = true;
        string endTime = TimeSpan.FromSeconds(currentSeconds).ToString(@"hh\:mm\:ss");
        endTimerText.text = endTime;
        movement.enabled = false;
        rotation.enabled = false;
        //save final time for scoreboard
        PlayerPrefs.SetString("Time", endTime);
    }
}
