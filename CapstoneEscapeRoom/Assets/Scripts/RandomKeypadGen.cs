// Name: Matthew Tucker 
// Description: Randomly select texter (4 options - #ff65a3, #fff740, #feff9c,#7afcff) and generate a password and put both on stickynote (for keypad)
// possible random locations (has to be set by game developer) 
// Date: 1/10/2023, 1/24/2023

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.FilePathAttribute;


class RandomKeypadGen : MonoBehaviour
{

    public TMP_Text outputs; // connect TMP text to outputs in code

    public Material[] Materials; // materials list / colors 
    public Renderer rend;
    public string outputString;

    public Vector3[] Locations; // locations the object could be (x,y,z)

    public void Start()
    {
        // select color 
        int n = UnityEngine.Random.Range(0, Materials.Length);// select material 
        rend.enabled = true;
        rend.sharedMaterial = Materials[n];

        if (Locations.Length > 0) // if more then 0 locations to place object 
        {
            int y = UnityEngine.Random.Range(0, Locations.Length);// select position 
            transform.position = Locations[y]; // set position 
        }



        string output = ""; // starting output of nothing

        string possibleInString = "0123456789"; // possible characters 
        char[] possibleInput = possibleInString.ToCharArray(); // convert to char list 

        int x = 0;// starting input 
        int length = UnityEngine.Random.Range(4, 6); // length of password range

        var rand = new System.Random(); // set up random

        while (x < length)
        { // loop untile password done 
            int input = rand.Next(0, possibleInput.Length);// random between 0 and max len of character
            output = output + possibleInput[input]; // put into string format 
            x++;
        }

        outputs.text = output; // send to text mesh pro
        outputString = output;
    }
}