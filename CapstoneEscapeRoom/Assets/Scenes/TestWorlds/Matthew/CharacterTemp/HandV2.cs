using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class HandV2 : MonoBehaviour
{
    // Animation 
    [SerializeField] private float animationSpeed;

    Animator animator;
    SkinnedMeshRenderer mesh;
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";

    // Physics Movement 
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpped = 100f;
    [SerializeField] private Vector3 positionOffest;
    [SerializeField] private Vector3 rotationOffset;
    private Transform _followTargert;
    private Rigidbody _body; 


    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    // Start is called before the first frame update
    void Start()
    {
        // animation 
        animator = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        // physics movement 
        _followTargert = followObject.transform;
        _body = GetComponent<Rigidbody>();
        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;

        // teleport hands 
        _body.position = _followTargert.position;
        _body.rotation = _followTargert.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();

        PhysicsMove();
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }

        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }
    }

    void PhysicsMove()
    {
        // postion 
        var positionWithOffset = _followTargert.TransformPoint(positionOffest);
        var distance = Vector3.Distance(positionWithOffset, transform.position);

        // rotation 
        var rotationWithOffset = _followTargert.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpped);
    }
}
