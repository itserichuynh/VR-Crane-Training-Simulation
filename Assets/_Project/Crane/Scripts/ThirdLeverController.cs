using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLeverController : MonoBehaviour
{
    Vector3 leverRotation;
    public bool leverThreeActive;

    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        //Debug.Log(leverRotation.x);


        if ((leverRotation.x <= 20f && leverRotation.x >= 0f) || (leverRotation.x >= 340f && leverRotation.x <= 360f))
        {
            ExtendBoom.Instance.ExtendStationary();
            leverThreeActive = false;
        }
        else if (leverRotation.x > 20f && leverRotation.x <= 40f)
        {
            ExtendBoom.Instance.BoomShorten(0.05f);
            leverThreeActive = true;
        }
        else if (leverRotation.x > 40f && leverRotation.x <= 61f)
        {
            ExtendBoom.Instance.BoomShorten(0.1f);
            leverThreeActive = true;
        }
        else if (leverRotation.x < 340f && leverRotation.x >= 320f)
        {
            ExtendBoom.Instance.BoomExtend(0.05f);
            leverThreeActive = true;
        }
        else if (leverRotation.x < 320f && leverRotation.x >= 299f)
        {
            ExtendBoom.Instance.BoomExtend(0.1f);
            leverThreeActive = true;
        }
    }
}
