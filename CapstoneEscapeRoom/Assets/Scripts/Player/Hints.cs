using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints : MonoBehaviour
{
    public int negativePoints = 100;
    public int hintsTotal;
    public int level;
    public string task;
    public string hint;
    // Start is called before the first frame update


    void useHint()
    {
        hintsTotal = PlayerPrefs.GetInt("Hints");

        hintsTotal += negativePoints;

        PlayerPrefs.SetInt("Hints", hintsTotal);
        displayHint();
    }

    void displayHint()
    {
        level = PlayerPrefs.GetInt("Level");
        //task = 
        //show hint to player based on level and task
    }
   
}
