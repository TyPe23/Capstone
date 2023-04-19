using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveChanger : MonoBehaviour
{
    // current movment 
    public bool continuousMovment = true;
    public bool telportMovment = false;
    // gameObjects 
    public ActionBasedContinuousTurnProvider continuousMove1;
    public ActionBasedContinuousMoveProvider continuousMove2;
    public ActionBasedSnapTurnProvider telportMove1;
    public TeleportationProvider telportMove2;

    public XRInteractorLineVisual line;
    public float defaultLength = 0.5f;
    public XRRayInteractor linePart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // chaning
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
        }
        else
        {
            continuousMovment = true;
            telportMovment = false;
            continMoving();
            line.lineLength = defaultLength;
            linePart.lineType = XRRayInteractor.LineType.StraightLine;
            linePart.interactionLayers = ~0;
        }
    }

    void continMoving()
    {
        continuousMove1.enabled = true;
        continuousMove2.enabled = true;
        telportMove1.enabled = false;
        telportMove2.enabled = false;
    }

    void telMoving()
    {
        continuousMove1.enabled = false;
        continuousMove2.enabled = false;
        telportMove1.enabled = true;
        telportMove2.enabled = true;
    }
}
