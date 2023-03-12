using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Lockpick : MonoBehaviour
{
    public Transform tumbler;
    public Transform pickPosition;
    public GameObject pick;
    public Camera cam;
    public GameObject door;

    public float maxAngle = 90;

    public float lockSpeed = 10;

    [Min(1)]
    [Range(1, 25)]
    public float lockRange = 10;

    public float eulerAngle;
    public float unlockAngle;
    public Vector2 unlockRange;
    public Vector3 axisAngle;
    public GameObject trackedHand;

    private float keyPressTime = 0;

    private bool movePick = true;

    // Start is called before the first frame update
    void Start()
    {
        unlockAngle = Random.Range(-maxAngle + lockRange, maxAngle - lockRange);
        unlockRange = new Vector2(unlockAngle - lockRange, unlockAngle + lockRange);
        axisAngle = tumbler.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        pick.transform.position = pickPosition.position;

        if (movePick)
        {
            Vector3 dir = new Vector3(trackedHand.transform.localPosition.x, trackedHand.transform.localPosition.y, 0) - pick.transform.localPosition;

            //dir = new Vector3(0, dir.y, dir.z);

            print(dir);

            eulerAngle = Vector3.Angle(dir, Vector3.up);

            Vector3 cross = Vector3.Cross(Vector3.up, dir);

            if (cross.z < 0)
            {
                eulerAngle = -eulerAngle;
            }
            
            eulerAngle = Mathf.Clamp(eulerAngle, -maxAngle, maxAngle);


            Quaternion rotateTo = Quaternion.AngleAxis(eulerAngle, Vector3.forward);

            pick.transform.rotation = Quaternion.Euler(axisAngle) * rotateTo;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            movePick = false;
            keyPressTime = 1;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            movePick = true;
            keyPressTime = 0;
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
                door.GetComponent<XRGrabInteractable>().enabled = true;
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
