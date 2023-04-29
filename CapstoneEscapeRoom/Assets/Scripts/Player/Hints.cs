    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hints : MonoBehaviour
{
    public int negativePoints = 100;
    public int hintsTotal;
    public int level;
    public int task;
    public TMP_Text hint;
    public string output;
    public GameObject background;
    public bool hintUsed;
    public int currentTask;
    // Start is called before the first frame update

    
    public void useHint()
    {
        level = PlayerPrefs.GetInt("Level");
        task = PlayerPrefs.GetInt("Task");
        if (task != currentTask)
        {
            currentTask = task;
            hintUsed = false;
        }
        if (!hintUsed)
        {


            hintsTotal = PlayerPrefs.GetInt("Hints");

            hintsTotal += negativePoints;

            PlayerPrefs.SetInt("Hints", hintsTotal);

            displayHint();
            hintUsed = true;
        }
    }

    void displayHint()
    {

        background.SetActive(true);
        switch (level)
        {
            case 1: //level 1
                switch(task)
                {
                    case 3:
                        output = "There is a keycard that can be used to enter the manger's office";
                        break;
                    case 2:
                        output = "There is a note pad in the manger's office that has the passcode to enter the server room";
                        break;
                    case 1:
                        output = "Use the help command on the terminal for useful commands";
                        break;
                }
                break;
            case 2: //level
                switch (task)
                {
                    case 4:
                        output = "Lockpick into the janitor's office and use the keycard inside to get into the manger's office";
                        break;
                    case 3:
                        output = "The password to the manger's computer can be found in the meeting room, it is a ceasar cipher";
                        break;
                    case 2:
                        output = "answers to the security questions can be found around the office";
                        break; 
                    case 1:
                        output = "rm is the delete command";
                        break;
                }
                break;
            case 3:
                switch (task)
                {
                    case 3:
                        output = "the key card can be found in the cabnet";
                        break;
                    case 2:
                        output = "find blinking port and plug into computer with cable";
                        break;
                    case 1:
                        output = "find a exe maybe use cd on the new file";
                        break;

                }
                break;
        }
        
        hint.text = output;
        //task = 
        //show hint to player based on level and task
    }
   
}
