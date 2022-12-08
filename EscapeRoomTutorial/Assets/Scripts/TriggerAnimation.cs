using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerAnimation : XRSimpleInteractable
{

    public string openAnimation;
    public string closeAnimation;
    private Animator animator;
    private bool enable = false;

    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        enable = !enable;

        if (enable)
        {
            animator.Play(openAnimation);
        }
        else
        {
            animator.Play(closeAnimation);
        }
    }
}
