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
    public Material[] Materials; // materials list / colors
    public Renderer rend;// renders 

    public GameObject doorLock2; // the door grab object 
    public GameObject trackedObjecct;
    public GameObject reader;
    public float minDist;

    public AudioSource audio;
    public TaskList UI;

    private void Update()
    {
        float dist = Vector3.Distance(reader.transform.position, trackedObjecct.transform.position);
        if (dist <= minDist)
        {
            rend.enabled = true; // enable rendering change 
            rend.sharedMaterial = Materials[1]; // change material 
            doorLock2.GetComponent<XRGrabInteractable>().enabled = true; // unlock door
            if (UI != null)
            {
                UI.taskDone(1);                                              // compleate task 
            }
            audio.Play(0);
        }
    }
}
