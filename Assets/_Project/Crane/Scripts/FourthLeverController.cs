using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthLeverController : Singleton<FourthLeverController>
{
    public Vector3 leverRotation;
    public bool leverFourActive;

    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        //Debug.Log(leverRotation.x);


        if ((leverRotation.x <= 10f && leverRotation.x >= 0f) || (leverRotation.x >= 350f && leverRotation.x <= 360f))
        {
            HookController.Instance.HookStationary();
            HookController.Instance.hookIsMoving = false;
            leverFourActive = false;
        }
        else if (leverRotation.x > 10f && leverRotation.x <= 61f)
        {
            HookController.Instance.HookUp();
            HookController.Instance.hookIsMoving = true;
            leverFourActive = true;
        }
        else if (leverRotation.x < 350f && leverRotation.x >= 299f)
        {
            HookController.Instance.HookDown();
            HookController.Instance.hookIsMoving = true;
            leverFourActive = true;
        }
    }

}
