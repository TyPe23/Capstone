using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Lockpick : MonoBehaviour
{
    public Transform NormalRef;
    public Transform tumbler;
    public Transform pickPosition;
    public GameObject pick;
    public Camera cam;
    public GameObject door;
    public GameObject trackedHand;

    [Min(1)]
    [Range(1, 25)]
    public float lockRange = 10;

    private float maxAngle = 90;
    private float lockSpeed = 10;
    private float eulerAngle;
    private float unlockAngle;
    private Vector2 unlockRange;
    private Vector3 axisAngle;
    private float keyPressTime = 0;
    private bool movePick = true;
    private Vector3 Normal;
    private XRGrabInteractable doorGrab;
    private Rigidbody doorRB;

    // Start is called before the first frame update
    void Start()
    {
        unlockAngle = Random.Range(-maxAngle + lockRange, maxAngle - lockRange);
        unlockRange = new Vector2(unlockAngle - lockRange, unlockAngle + lockRange);
        axisAngle = tumbler.transform.eulerAngles;
        Normal = Vector3.Normalize(NormalRef.position - tumbler.position);
        doorGrab = door.GetComponent<XRGrabInteractable>();
        doorRB = door.GetComponent<Rigidbody>();
    }

    public void TurnLock(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            movePick = false;
            keyPressTime = 1;
        }
        else
        {
            movePick = true;
            keyPressTime = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        pick.transform.position = pickPosition.position;

        if (movePick)
        {
            Vector3 dir = (trackedHand.transform.position - tumbler.position);
            Vector3 projectedDir = dir - (Vector3.Dot(dir, Normal) / (Mathf.Pow(Vector3.Magnitude(Normal), 2)) * Normal);
            projectedDir = new Vector3(-projectedDir.x, projectedDir.y, projectedDir.z);

            eulerAngle = Vector3.Angle(projectedDir, Vector3.up);

            Vector3 cross = Vector3.Cross(Vector3.up, projectedDir);

            if (cross.z < 0)
            {
                eulerAngle = -eulerAngle;
            }
            
            eulerAngle = Mathf.Clamp(eulerAngle, -maxAngle, maxAngle);


            Quaternion rotateTo = Quaternion.AngleAxis(eulerAngle, Vector3.forward);

            pick.transform.rotation = Quaternion.Euler(axisAngle) * rotateTo;
        }

        

        keyPressTime = Mathf.Clamp(keyPressTime, 0, 1);

        float percentage = Mathf.Round(100 - Mathf.Abs(((eulerAngle - unlockAngle) / 100) * 100));
        percentage = Mathf.Clamp(percentage, 0, 100);
        float lockRotation = ((percentage / 100) * maxAngle) * keyPressTime;
        float maxRotation = (percentage / 100) * maxAngle;

        float lockLerp = Mathf.Lerp(tumbler.eulerAngles.z, lockRotation, Time.deltaTime * lockSpeed);

        tumbler.eulerAngles = new Vector3(0, 0, lockLerp) + axisAngle;

        if(lockLerp >= maxRotation - 1)
        {
            if (eulerAngle < unlockRange.y && eulerAngle > unlockRange.x)
            {
                Debug.Log("unlocked");
                doorGrab.enabled = true;
                doorRB.isKinematic = false;
                Destroy(gameObject);

                movePick = true;
                keyPressTime = 0;
            }
            else
            {
                float randomRotation = Random.insideUnitCircle.x;
                pick.transform.eulerAngles += new Vector3(0, 0, Random.Range(-randomRotation, randomRotation));
            }
        }
    }
}
