using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLeverController : Singleton<FirstLeverController>
{
    public Vector3 leverRotation;
    public bool leverOneActive;
    Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        //Debug.Log(leverRotation.x);
        //Debug.Log(_rb.angularVelocity);

        if ((leverRotation.x <= 15f && leverRotation.x >= 0f) || (leverRotation.x >= 345f && leverRotation.x <= 360f))
        {
            CabRotation.Instance.CabStationary();
            CabRotation.Instance.cabIsTurning = false;
            leverOneActive = false;
        }
        else if (leverRotation.x > 15f && leverRotation.x <= 61f)
        {
            CabRotation.Instance.CabTurnLeft();
            CabRotation.Instance.cabIsTurning = true;
            leverOneActive = true;
        }
        else if (leverRotation.x < 345f && leverRotation.x >= 299f)
        {
            CabRotation.Instance.CabTurnRight();
            CabRotation.Instance.cabIsTurning = true;
            leverOneActive = true;
        }  
    }
}
