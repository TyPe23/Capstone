using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elevatordoor : MonoBehaviour
{

    private Animation anim;
    public GameObject ob;

    public void PlayAnimation()
    {
        anim = ob.GetComponent<Animation>();
        anim.Play("Elevator");

       
    }
}
