// Name: Ty Pederson
// Description: Handles levelselection       
// Date: 2/4/2023

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorButton : MonoBehaviour
{
    public GameObject [] buttons;
    bool open = false;
    public AudioDoor sound;
    public TMP_Text text; 

    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].activeSelf == false)
            {
                if (!open)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                    GetComponent<BoxCollider>().isTrigger = true;
                    sound.open();
                    open = true;
                }

                text.text = "Enter to go to\n Level " + (i + 1);
            }
        }
    }

    public void OnTriggerEnter(Collider other) // for button on level selection 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].activeSelf == false)
                {
                    SceneManager.LoadScene(i + 2);// load a level
                }
            }
        }
    }
}
