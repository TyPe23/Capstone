using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Matthew Tucker 
// Date: 4/20/23
//Description: Toggle to switch between continous and teleporting movment 
public class MoveChanger : MonoBehaviour
{
    // current movment 
    public bool continuousMovment = true;
    public bool telportMovment = false;
    // movement system
    public ActionBasedContinuousTurnProvider continuousMove1;
    public ActionBasedContinuousMoveProvider continuousMove2;
    //public ActionBasedSnapTurnProvider telportMove1;
    public TeleportationProvider telportMove2;
    // left hand collider for when teleporting to prevent handing going though wall 
    public BoxCollider LeftHandCollider;
    public GameObject LeftHand;
    // defaults and settings 
    public XRInteractorLineVisual line;
    public float defaultLength = 0.5f;
    public XRRayInteractor linePart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // chaning systyem 
    public void changMovemnet()
    {
        if (continuousMovment)
        {
            continuousMovment = false;
            telportMovment = true;
            telMoving();
            line.lineLength = 10;
            linePart.lineType = XRRayInteractor.LineType.ProjectileCurve;
            linePart.velocity = 5;
            linePart.interactionLayers = LayerMask.GetMask("Default");
            LeftHand.layer = LayerMask.GetMask("Default");
        }
        else
        {
            continuousMovment = false;
            telportMovment = true;
            continMoving();
            line.lineLength = defaultLength;
            linePart.lineType = XRRayInteractor.LineType.StraightLine;
            linePart.interactionLayers = ~0;
            LeftHand.layer = LayerMask.GetMask("Player");
        }
    }

    // change to continous moving
    void continMoving()
    {
        continuousMove1.enabled = true;
        continuousMove2.enabled = true;
        //telportMove1.enabled = false;
        telportMove2.enabled = false;
        LeftHandCollider.enabled = false;
    }

    // change to tel moving 
    void telMoving()
    {
        continuousMove1.enabled = false;
        continuousMove2.enabled = false;
        //telportMove1.enabled = true;
        telportMove2.enabled = true;
        LeftHandCollider.enabled |= true;
    }
}
