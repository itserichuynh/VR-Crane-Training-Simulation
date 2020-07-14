using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : Singleton<HookController>
{
    private ConfigurableJoint configurableJoint;
    private SoftJointLimit softJointLimit;

    private float minHookPosition = 0.0f;
    private float maxHookPosition = 10f;

    private float currentLength;

   
    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        softJointLimit = new SoftJointLimit();
    }

    private void FixedUpdate()
    {
        currentLength = softJointLimit.limit;
    }

    public void HookUp(float hookSpeed)
    {
        softJointLimit.limit = Mathf.Clamp(softJointLimit.limit - hookSpeed, minHookPosition, maxHookPosition);
        configurableJoint.linearLimit = softJointLimit;
    }

    public void HookDown(float hookSpeed)
    {
        softJointLimit.limit = Mathf.Clamp(softJointLimit.limit + hookSpeed, minHookPosition, maxHookPosition);
        configurableJoint.linearLimit = softJointLimit;
    }

    public void HookStationary()
    {
        softJointLimit.limit = currentLength;
        configurableJoint.linearLimit = softJointLimit;
    }

}
