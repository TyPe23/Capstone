// Name: Matthew Tucker
// Data: 1/16/2023
// Description: Handles keypad input and checking 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;

public class keypadControler : MonoBehaviour
{
    string textOut = null; // output 
    public int wordIndex = 0;// size of output 
    public TMP_Text output = null; // text display
    public GameObject inputField; // where to get correct number 
    //string[] inputs = { "ent", "0", "del", "1", "2", "3", "4", "5", "6", "7", "8", "9" }; // valid inputs (never used) 
    private string textIn; // correct input - make public for testing else private 

    public Material[] Materials; // materials list / colors 
    public Renderer rend;// renders 
    public AudioUnlock unlock;
    public AudioDenied denied;
    public GameObject doorLock; // the door grab object 

    public TaskList UI;

    void Start() { // get input 
        textIn = inputField.GetComponent<TMP_Text>().text;  
    }   
    
    
    

    public void typingFunct(string letter) // input letters 
    {
        wordIndex++;
        textOut = textOut + letter;
        output.text = textOut; // send to text mesh pro
    }


    public void backspaceFunct()
    {
        //only erase a character if there are characters to erase
        if (wordIndex > 0)
        {
            //subtract a character from the word
            textOut = textOut.Substring(0, wordIndex - 1);
            wordIndex--;
            output.text = textOut;
        }
    }

    public void enterFunct() // enter and check if correct 
    {
        textIn = inputField.GetComponent<TMP_Text>().text; // fixes problem where sometimes this code is ran too soon/fast and does not pick up the correct code 
        if (textOut == textIn) // if correct 
        {
            rend.enabled = true; // enable rendering change 
            rend.sharedMaterial = Materials[0]; // change material 
            doorLock.GetComponent<XRGrabInteractable>().enabled = true; // unlock door
            doorLock.GetComponent<Rigidbody>().isKinematic = false;
            UI.taskDone(2);
            unlock.unlock();
        }
        else // if wrong 
        {
            rend.enabled = true; // enable rendering chage 
            rend.sharedMaterial = Materials[1]; // change material 
            doorLock.GetComponent<XRGrabInteractable>().enabled = false;// lock door
            denied.deny();
        }
    }

}
