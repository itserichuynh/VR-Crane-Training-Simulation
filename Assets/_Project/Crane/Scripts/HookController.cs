using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : Singleton<HookController>
{
    public float hookSpeed;

    private SpringJoint springJoint;
    private SoftJointLimit softJointLimit;

    private float minHookPosition = 0.5f;
    private float maxHookPosition = 10f;

    private float currentLength;

    public bool hookIsMoving;

   
    void Start()
    {
        hookSpeed = 0.005f;
        springJoint = GetComponent<SpringJoint>();
    }

    private void FixedUpdate()
    {
        currentLength = springJoint.maxDistance;
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
