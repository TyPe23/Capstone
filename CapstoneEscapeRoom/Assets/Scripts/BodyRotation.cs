using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    public GameObject camera;
    public GameObject toe;
    private float fromAngle;
    private float toAngle;
    private float deltaAngle;
    private string rotateParam = "hmdRotation";

    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        fromAngle = camera.transform.rotation.eulerAngles.y;
        toAngle = toe.transform.rotation.eulerAngles.y;

        deltaAngle = fromAngle - toAngle;

        animator.SetFloat(rotateParam, deltaAngle);
    }
}
