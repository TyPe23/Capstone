using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    // target of where the hands need to be 
    public Transform target;
    public Rigidbody rb;  // there rigidbody

    void Start()
    {
        rb=GetComponent<Rigidbody>(); // get rigidbody 
    }

    
    void FixedUpdate() // update where the hands should be 
    {
        // position 
        rb.velocity = (target.position - transform.position)/Time.fixedDeltaTime;
        //rotation 
        Quaternion rotationDifference = target.rotation*Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree*Mathf.Deg2Rad/Time.fixedDeltaTime);
    }
}
