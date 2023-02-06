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

    public void LevelSet()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }

        Debug.Log("Button Pressed");

        button.SetActive(false);
    }

}
