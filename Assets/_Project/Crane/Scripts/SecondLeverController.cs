using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondLeverController : MonoBehaviour
{
    Vector3 leverRotation;
    public bool leverTwoActive;

    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        //Debug.Log(leverRotation.x);
        

        if ((leverRotation.x <= 20f && leverRotation.x >= 0f) || (leverRotation.x >= 340f && leverRotation.x <= 360f))
        {
            BoomRaise.Instance.BoomStationary();
            leverTwoActive = false;
        }
        else if (leverRotation.x > 20f && leverRotation.x <= 40f)
        {
            BoomRaise.Instance.LowerBoom(10f);
            leverTwoActive = true;
        }
        else if (leverRotation.x > 40f && leverRotation.x <= 61f)
        {
            BoomRaise.Instance.LowerBoom(15f);
            leverTwoActive = true;
        }
        else if (leverRotation.x < 340f && leverRotation.x >= 320f)
        {
            BoomRaise.Instance.RaiseBoom(10f);
            leverTwoActive = true;
        }
        else if (leverRotation.x < 320f && leverRotation.x >= 299f)
        {
            BoomRaise.Instance.RaiseBoom(15f);
            leverTwoActive = true;
        }        
    }
}
