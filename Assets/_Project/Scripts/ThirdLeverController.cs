using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdLeverController : Singleton<ThirdLeverController>
{
    public Vector3 leverRotation;
    public bool leverThreeActive;

    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        //Debug.Log(leverRotation.x);


        if ((leverRotation.x <= 15f && leverRotation.x >= 0f) || (leverRotation.x >= 345f && leverRotation.x <= 360f))
        {
           
            ExtendBoom.Instance.ExtendStationary();
            ExtendBoom.Instance.boomIsExtending = false;
            leverThreeActive = false;
        }
        else if (leverRotation.x > 15f && leverRotation.x <= 61f)
        {
            if (ExtendBoom.Instance.position.z > 0.081f)
            {
                ExtendBoom.Instance.BoomShorten();
                ExtendBoom.Instance.boomIsExtending = true;
                leverThreeActive = true;
            }
            else
            {
                ExtendBoom.Instance.ExtendStationary();
                ExtendBoom.Instance.boomIsExtending = false;
                leverThreeActive = true;
                //Debug.Log("No force1");
            }
                
        }
        else if (leverRotation.x < 345f && leverRotation.x >= 299f)
        {
            if (ExtendBoom.Instance.position.z < 3.581f)
            {
                ExtendBoom.Instance.BoomExtend();
                ExtendBoom.Instance.boomIsExtending = true;
                leverThreeActive = true;
            }
            else
            {
                ExtendBoom.Instance.ExtendStationary();
                ExtendBoom.Instance.boomIsExtending = false;
                leverThreeActive = true;
                //Debug.Log("No force3");
            }

        }
    }
}
