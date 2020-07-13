using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : Singleton<HookController>
{
    public KeyCode Hook_Down_keyA;
    public KeyCode Hook_keyA_B;
    public KeyCode Hook_Up_keyB;

    public float hookSpeed = 0.01f;

    private ConfigurableJoint configurableJoint;
    private SoftJointLimit softJointLimit;

    private float minHookPosition = 0.5f;
    private float maxHookPosition = 5f;

   
    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        softJointLimit = new SoftJointLimit();
        softJointLimit.limit = 1f;
        configurableJoint.linearLimit = softJointLimit;

    }

    void FixedUpdate()
    {
        if (Input.GetKey(Hook_Down_keyA) && Input.GetKey(Hook_keyA_B))
        {
            HookDown();
        }
        else if (Input.GetKey(Hook_Up_keyB) && Input.GetKey(Hook_keyA_B))
        {
            HookUp();
        }
    }

    public void HookUp()
    {
        softJointLimit.limit = Mathf.Clamp(softJointLimit.limit - hookSpeed, minHookPosition, maxHookPosition);
        configurableJoint.linearLimit = softJointLimit;
    }

    public void HookDown()
    {
        softJointLimit.limit = Mathf.Clamp(softJointLimit.limit + hookSpeed, minHookPosition, maxHookPosition);
        configurableJoint.linearLimit = softJointLimit;
    }

}
