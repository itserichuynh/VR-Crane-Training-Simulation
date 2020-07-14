using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : Singleton<HookController>
{
    private SpringJoint springJoint;
    private SoftJointLimit softJointLimit;

    private float minHookPosition = 0.1f;
    private float maxHookPosition = 10f;

    private float currentLength;

   
    void Start()
    {
        springJoint = GetComponent<SpringJoint>();
    }

    private void FixedUpdate()
    {
        currentLength = springJoint.maxDistance;
    }

    public void HookUp(float hookSpeed)
    {
        springJoint.maxDistance = Mathf.Clamp(springJoint.maxDistance - hookSpeed, minHookPosition, maxHookPosition);
    }

    public void HookDown(float hookSpeed)
    {
        springJoint.maxDistance = Mathf.Clamp(springJoint.maxDistance + hookSpeed, minHookPosition, maxHookPosition);
    }

    public void HookStationary()
    {
        springJoint.maxDistance = currentLength;
    }

}
