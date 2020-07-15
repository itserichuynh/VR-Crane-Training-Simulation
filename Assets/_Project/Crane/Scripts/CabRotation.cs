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

    private void FixedUpdate()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    public void CabTurnRight(float cabRotateSpeed)
    {
        _rb.AddTorque(Vector3.up * cabRotateSpeed);
        //Vector3 m_EulerAngleVelocity = new Vector3(0, cabRotateSpeed, 0);
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        //_rb.MoveRotation(_rb.rotation * deltaRotation);
    }

    public void CabTurnLeft(float cabRotateSpeed)
    {
         _rb.AddTorque(-Vector3.up * cabRotateSpeed);
        //Vector3 m_EulerAngleVelocity = new Vector3(0, -cabRotateSpeed, 0);
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        //_rb.MoveRotation(_rb.rotation * deltaRotation);
    }

    public void CabStationary()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
