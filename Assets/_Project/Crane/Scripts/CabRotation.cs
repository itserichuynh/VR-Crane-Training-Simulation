using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabRotation : Singleton<CabRotation>
{
    //public float cabRotateSpeed; // 120

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    public void CabTurnRight(float cabRotateSpeed)
    {
        _rb.AddTorque(Vector3.up * cabRotateSpeed);
    }

    public void CabTurnLeft(float cabRotateSpeed)
    {
        _rb.AddTorque(-Vector3.up * cabRotateSpeed);
    }

    public void CabStationary()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
