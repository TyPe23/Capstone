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
    // Start is called before the first frame update


    public void useHint()
    {
        hintsTotal = PlayerPrefs.GetInt("Hints");

        hintsTotal += negativePoints;

        PlayerPrefs.SetInt("Hints", hintsTotal);
        displayHint();
    }

    void displayHint()
    {
        level = PlayerPrefs.GetInt("Level");
        task = PlayerPrefs.GetInt("Task");
        background.SetActive(true);
        switch (level)
        {
            case 1:
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
            case 2:
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
        }
        hint.text = output;
        //task = 
        //show hint to player based on level and task
    }
   
}
