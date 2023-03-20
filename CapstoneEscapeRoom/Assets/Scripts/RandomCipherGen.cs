// Name: Benjamin Sanguinetti 
// Description: picks a random password and encrpyts it in rot-13 displays on poster
// 
// Date: 3/7/2023

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



class RandomCipherGen : MonoBehaviour
{

    public TMP_Text outputs; // connect TMP text to outputs in code
    public TMP_Text rots;

    //public Material[] Materials; // materials list / colors 
    public Renderer rend;
    public string outputString;


    public void Start()
    {
   


  
        int rot = 13;
        string[] possibleInString = {"Jiggsaw", "HexKing", "CaesarsCipher",  "ZeroTrust", "DunderMifflin" };
        int r = UnityEngine.Random.Range(0, possibleInString.Length);
       
        char[] s = possibleInString[r].ToCharArray();
        
        for (int i = 0; i < possibleInString[r].Length; i++)
        {
            int number = (int)s[i];

            if (number >= 'a' && number <= 'z')
            {
                if (number > 'm')
                {
                    number -= rot;
                }
                else
                {
                    number += rot;
                }
            }
            else if (number >= 'A' && number <= 'Z')
            {
                if (number > 'M')
                {
                    number -= rot;
                }
                else
                {
                    number += rot;
                }
            }
            s[i] = (char)number;
        }

        string output = new string(s);
        string shift = "a -> n";

        rots.text = shift;
        outputs.text = output; // send to text mesh pro
        outputString = output;
    }
}