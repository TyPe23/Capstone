// Name: Matthew Tucker 
// Date: 1/14/23
// Discription: Handles pause menue pull up and function 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    // controler 
    public GameObject PauseUI;
    //  bool about if active 
    public bool activePauseUI = true; 
    
    void Start() // on start up 
    {
        DisplayWristUI(); 
    }

    // change if displaying or not
    public void DisplayWristUI()
    {
        if(activePauseUI)
        {
            PauseUI.SetActive(false);
            activePauseUI = false;
        }
        else if (!activePauseUI)
        {
            PauseUI.SetActive(true);
            activePauseUI = true;
        }
    }
    // pause button if button clicked and removal of pause menue if clicked 
    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayWristUI();
        }
    }

    public void restartLevel() // restart level 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void exitLevel() // return to level selection function 
    {
        SceneManager.LoadScene(1); // retunrs to level selection - NOTE: Might want to add a failed and time in game if done 
    }

    public void exitGame() // leave game 
    {
        Application.Quit();
    }
}
