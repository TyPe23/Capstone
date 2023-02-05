// Name: Matthew Tucker
// Date: 1/15/2023
// Description: adding the ability to press a button and turn on/of a floating keyboard 

using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyBoardSwitch : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f; // when to click passed this  
    [SerializeField] private float deadZone = 0.025f; // deadzone

    public UnityEvent onPressed; // enable the ability to capture on pressed events 
    

    //private bool isPressed = false; // be able to determin if turning on or off 
    private Vector3 startPos; // starting position 
    private ConfigurableJoint joint; // the joint being used 
    public GameObject keyboard; // keyboard object 
    public bool value; // true or false 

    private void Start() // initalize 
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
        //keyboard = GameObject.Find("KeyboardObject");
    }

    private void Update()
    {
        if(GetValue() + threshold >= 1)
        {
            Pressed();
        }
    }

    private void Pressed()
    {
        keyboard.SetActive(value);
    }

    private float GetValue() // get the current value of button (how much pressed) 
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit; // get distance 
        if (Mathf.Abs(value) < deadZone) // check if less than deadzone if so 0 
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f); // return a value between -1 and 1 

    }
}
