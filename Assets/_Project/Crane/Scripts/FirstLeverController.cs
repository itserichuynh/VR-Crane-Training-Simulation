using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLeverController : MonoBehaviour
{
    Vector3 leverRotation;
    float xAngle;

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;

    //private void Update()
    //{
    //    leverRotation = transform.localEulerAngles;
    //}

    private void FixedUpdate()
    {
        leverRotation = transform.localEulerAngles;
        Debug.Log(leverRotation.x);

        if ((leverRotation.x <= 5f && leverRotation.x >= 0f) || (leverRotation.x >= 355f && leverRotation.x <= 360f))
        {
            CabRotation.Instance.CabStationary();
            //Debug.Log("First");
        }
        else if (leverRotation.x > 5f && leverRotation.x <= 61f)
        {
            CabRotation.Instance.CabTurnLeft();
            //Debug.Log("Sec");
        }
        else if (leverRotation.x < 355f && leverRotation.x >= 299f)
        {
            CabRotation.Instance.CabTurnRight();
            //Debug.Log("Third");
        }
    }
}
