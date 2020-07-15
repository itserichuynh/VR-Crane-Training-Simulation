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


        if ((leverRotation.x <= 10f && leverRotation.x >= 0f) || (leverRotation.x >= 350f && leverRotation.x <= 360f))
        {
           
            ExtendBoom.Instance.ExtendStationary();
            leverThreeActive = false;
        }
        else if (leverRotation.x > 10f && leverRotation.x <= 61f)
        {
            if (ExtendBoom.Instance.position.z > 0.081f)
            {
                ExtendBoom.Instance.BoomShorten(0.1f);
                leverThreeActive = true;
            }
            else
            {
                ExtendBoom.Instance.ExtendStationary();
                //Debug.Log("No force1");
            }
                
        }
        else if (leverRotation.x < 350f && leverRotation.x >= 299f)
        {
            if (ExtendBoom.Instance.position.z < 3.081f)
            {
                ExtendBoom.Instance.BoomExtend(0.1f);
                leverThreeActive = true;
            }
            else
            {
                ExtendBoom.Instance.ExtendStationary();
                //Debug.Log("No force3");
            }

        }
    }
}
