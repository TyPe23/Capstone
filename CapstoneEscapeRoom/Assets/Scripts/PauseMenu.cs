// Name: Matthew Tucker 
// Date: 1/14/23
// Discription: Handles pause menue pull up and function 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PauseMenu : MonoBehaviour
{
    // controler 
    public GameObject PauseUI;
    public GameObject Locamotion;
    //  bool about if active 
    public bool activePauseUI = true;
    public LocomotionSystem movement;
    public ActionBasedContinuousMoveProvider rotation;
    public AudioSource audioData;

    void Start() // on start up 
    {
        DisplayWristUI(); 
        
        // get locomation system 
        //movement = Locamotion.GetComponent<LocomotionSystem>();
        //rotation = Locamotion.GetComponent<ActionBasedContinuousMoveProvider>();
    }

    // change if displaying or not
    public void DisplayWristUI()
    {
        audioData.Play(0);
        if (activePauseUI)
        {
            //
            PauseUI.SetActive(false);
            movement.enabled = true;
            rotation.enabled = true;
            activePauseUI = false;
        }
        else if (!activePauseUI)
        {
           //audioData.Play(0);
            PauseUI.SetActive(true);
            movement.enabled = false;
            rotation.enabled = false;
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
