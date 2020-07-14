﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLeverController : MonoBehaviour
{
    Vector3 leverRotation;
    public bool leverOneActive;

    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        //Debug.Log(leverRotation.x);

        if ((leverRotation.x <= 20f && leverRotation.x >= 0f) || (leverRotation.x >= 340f && leverRotation.x <= 360f))
        {
            CabRotation.Instance.CabStationary();
            leverOneActive = false;
        }
        else if (leverRotation.x > 20f && leverRotation.x <= 40f)
        {
            CabRotation.Instance.CabTurnLeft(75f);
            leverOneActive = true;
        }
        else if (leverRotation.x > 40f && leverRotation.x <= 61f)
        {
            CabRotation.Instance.CabTurnLeft(80f);
            leverOneActive = true;
        }
        else if (leverRotation.x < 340f && leverRotation.x >= 320f)
        {
            CabRotation.Instance.CabTurnRight(75f);
            leverOneActive = true;
        }
        else if (leverRotation.x < 320f && leverRotation.x >= 299f)
        {
            CabRotation.Instance.CabTurnRight(80f);
            leverOneActive = true;
        }        
    }
}
