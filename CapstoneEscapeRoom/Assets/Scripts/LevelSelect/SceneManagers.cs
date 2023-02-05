// Name: Matthew Tucker 
// Description: Handles all buttons and changes from one scene to another 
// Date: 1/11/2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneManagers : MonoBehaviour
{
    // all for button on level selection 
    public GameObject button;
    public UnityEvent onPress, onReleased;
    bool isPressed = false;
    public int levelNumber;
    //

    public void playGame() // this is for main menu to go from there to level selection
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // move forward 1 Scene in Scene Manager
    }

    public void QuitGame() // close/quit game
    {
        Application.Quit();
    }

    public void ReturnMenu() // returns to main menu (scene 0)
    {
        SceneManager.LoadScene(0);
    }

    public void LevelSelectionMenu() // returns to level selection menue (scene 1)
    {
        SceneManager.LoadScene(1);
    }

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
        SceneManager.LoadScene(levelNumber + 1);// load a level
        Debug.Log("Load level next");
    }
    // Future functions here for level selection to go from level to level 
}
