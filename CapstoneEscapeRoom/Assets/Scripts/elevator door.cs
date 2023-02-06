using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class elevatordoor : MonoBehaviour
{
    public int i = 0;
    public Animator anim;

    public void PlayAnimation()
    {
        if (i < 1)
        {

            anim.Play("Elevator");
            i++;
        }
    }
}
