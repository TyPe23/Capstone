using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;
using System;
// Name: Benjamin Sanguinetti
// Data: 2/5/2023
// Description: Handles Key Card Reader

public class CardReader : MonoBehaviour
{
    //public Rigidbody _rb; //Rigidbody
    public Material[] Materials; // materials list / colors
    public Renderer rend;// renders 

    public GameObject doorLock2; // the door grab object 

    
    public TaskList UI;

    // Start is called before the first frame update
    void Start()
    {
        //_rb = this.GetComponent<Rigidbody>(); //assigne rigidbody to rb
    }

    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "ID card") //check what is colliding with the reader; if the it is the card unlock door
        {

            rend.enabled = true; // enable rendering change 
            rend.sharedMaterial = Materials[1]; // change material 
            doorLock2.GetComponent<XRGrabInteractable>().enabled = true; // unlock door
                                                                         // compleate task 
            UI.taskDone(1);
        }
        // else // if wrong 
        // {
        //     rend.enabled = true; // enable rendering chage 
        //     rend.sharedMaterial = Materials[0]; // change material 
        //     doorLock2.GetComponent<XRGrabInteractable>().enabled = false;// lock door
        // }
    }
}
