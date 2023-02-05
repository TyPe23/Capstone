// Name: Ty Pederson
// Description: Handles levelselection       
// Date: 2/4/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelPins : MonoBehaviour
{
    public GameObject button;
    public GameObject[] buttons;
    public UnityEvent onPress, onReleased;
    bool isPressed = false;

    public void OnTriggerEnter(Collider other) // for button on level selection 
    {
        if (!isPressed) // check if pressed 
        {
            for (int i = 0; i< buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }

            onPress.Invoke();
            isPressed = true; // changed that it is pressed 
            Debug.Log("Button Pressed");

            button.SetActive(false);
        }
    }
}
