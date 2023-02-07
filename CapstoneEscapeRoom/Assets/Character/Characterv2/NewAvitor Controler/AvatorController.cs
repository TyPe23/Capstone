// Name: Matthew Tucker
// Description: Controler for animation of character based on controllers 
// Date: 2/5/2023

using Unity.VisualScripting;
using UnityEngine;

[System.Serializable] // helper class 
public class MapTransforms // the Transformers for controlers and modeles IK 
{
    // the two targets (controler, model)
    public Transform vrTarget;
    public Transform ikTarget;

    // The Two offsets for tracking of position and rotation
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset; 

    // mapping function 
    public void VRMapping()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);// set model to controler location 
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset); // set model to controler rotation 
    }
}

public class AvatorController : MonoBehaviour
{
    // using helper class the head, and both arms 
    [SerializeField] private MapTransforms Head;
    [SerializeField] private MapTransforms LeftHand;
    [SerializeField] private MapTransforms RightHand;

    // other needed veriables 
    [SerializeField] private float turnSmoothness; 
    [SerializeField] Transform ikHead;
    [SerializeField] Vector3 HeadBodyOffset;

    // update each frame (Using late update to make smother) 
    private void Update()
    {
        // head movement 
        transform.position = ikHead.position + HeadBodyOffset; // offset 
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(ikHead.forward, Vector3.up).normalized, Time.deltaTime*turnSmoothness);
        // map to all
        Head.VRMapping();
        LeftHand.VRMapping();
        RightHand.VRMapping();
    }
}
