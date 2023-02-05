// Name: Matthew Tucker
// Description: Controller for head rotation 
// Date: 2/4/2023


using UnityEngine;

public class CharacterHead : MonoBehaviour
{
    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 possitionOffset, rotationOffset, headBodyOffset;


    private void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.up, Vector3.up).normalized;

        transform.position = followObject.TransformPoint(possitionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }

 
}
