// Name: Ty Pederson
// Description: Handles levelselection       
// Date: 2/4/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DoorButton : MonoBehaviour
{
    public GameObject Button;
    public GameObject [] buttons;
    public UnityEvent onPress, onReleased;
    bool isPressed = false;

    public void OnTriggerEnter(Collider other) // for button on level selection 
    {
        if (!isPressed) // check if pressed 
        {
            onPress.Invoke();
            isPressed = true; // changed that it is pressed 
            Debug.Log("Button Pressed");
        }
    }
    public void LevelLoad()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            if (buttons[i].activeSelf == false)
            {
                SceneManager.LoadScene(i + 2);// load a level
            }
        }

        Debug.Log("Load level next");
    }
    // Future functions here for level selection to go from level to level 
}
