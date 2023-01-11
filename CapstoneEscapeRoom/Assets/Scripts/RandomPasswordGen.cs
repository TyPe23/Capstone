// Name: Matthew Tucker 
// Description: Randomly select texter (4 options - #ff65a3, #fff740, #feff9c,#7afcff) and generate a password and put both on stickynote 
// Date: 1/10/2023

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


class RandomPasswordGen : MonoBehaviour
{

    public TMP_Text outputs; // connect TMP text to outputs in code

    public Material[] Materials; // materials list / colors 
    public Renderer rend;
    public string outputString;

    public void Start()
    {
        // select color 
        int n = UnityEngine.Random.Range(0, Materials.Length);// select material 
        rend.enabled= true;
        rend.sharedMaterial= Materials[n];



        string output = ""; // starting output of nothing

        string possibleInString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#*_-"; // possible characters 
        char[] possibleInput = possibleInString.ToCharArray(); // convert to char list 

        int x = 0;// starting input 
        int length = 10; // length of password

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
