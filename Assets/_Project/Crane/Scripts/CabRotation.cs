using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabRotation : Singleton<CabRotation>
{
    public float cabRotateSpeed; // initially, 120

    private Rigidbody _rb;

    public KeyCode Cab_Right_keyA;
    public KeyCode Cab_keyA_B;
    public KeyCode Cab_Left_keyB;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    public void CabTurnRight()
    {
        _rb.AddTorque(Vector3.up * cabRotateSpeed);
    }

    public void CabTurnLeft()
    {
        _rb.AddTorque(-Vector3.up * cabRotateSpeed);
    }

    public void CabStationary()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
