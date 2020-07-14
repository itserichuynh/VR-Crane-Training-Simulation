using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthLeverController : MonoBehaviour
{
    Vector3 leverRotation;
    public bool leverFourActive;

    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        //Debug.Log(leverRotation.x);


        if ((leverRotation.x <= 20f && leverRotation.x >= 0f) || (leverRotation.x >= 340f && leverRotation.x <= 360f))
        {
            HookController.Instance.HookStationary();
            leverFourActive = false;
        }
        else if (leverRotation.x > 20f && leverRotation.x <= 40f)
        {
            HookController.Instance.HookUp(0.005f);
            leverFourActive = true;
        }
        else if (leverRotation.x > 40f && leverRotation.x <= 61f)
        {
            HookController.Instance.HookUp(0.01f);
            leverFourActive = true;
        }
        else if (leverRotation.x < 340f && leverRotation.x >= 320f)
        {
            HookController.Instance.HookDown(0.005f);
            leverFourActive = true;
        }
        else if (leverRotation.x < 320f && leverRotation.x >= 299f)
        {
            HookController.Instance.HookDown(0.01f);
            leverFourActive = true;
        }
    }

}
