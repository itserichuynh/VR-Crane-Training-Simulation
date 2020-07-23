using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : Singleton<HookController>
{
    public float hookSpeed;
    public Vector3 position;
    public Vector3 worldPosition;
    public Vector3 originalPosition;

    public SpringJoint springJoint;
    private SoftJointLimit softJointLimit;

    private float minHookPosition = 0.5f;
    private float maxHookPosition = 10f;

    public float currentLength;

    public bool hookIsMoving;


    void Start()
    {
        hookSpeed = 0.005f;
        springJoint = GetComponent<SpringJoint>();
        originalPosition = this.transform.localPosition;
    }

    private void FixedUpdate()
    {
        currentLength = springJoint.maxDistance;
        position = transform.localPosition;
        worldPosition = transform.position;
    }

    public void HookUp()
    {
        springJoint.maxDistance = Mathf.Clamp(springJoint.maxDistance - hookSpeed, minHookPosition, maxHookPosition);
    }

    public void HookDown()
    {
        springJoint.maxDistance = Mathf.Clamp(springJoint.maxDistance + hookSpeed, minHookPosition, maxHookPosition);
    }

    public void HookStationary()
    {
        springJoint.maxDistance = currentLength;
    }

}
