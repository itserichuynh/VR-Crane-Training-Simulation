using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabRotation : Singleton<CabRotation>
{
    public float cabRotateSpeed; 
    public bool turn;
    public bool cabIsTurning;
    public Vector3 rotation;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        cabRotateSpeed = 100f;
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        rotation = transform.localEulerAngles;
        //Debug.Log(cabIsTurning);
    }

    public void CabTurnRight()
    {
        _rb.AddTorque(Vector3.up * cabRotateSpeed);
        /*
        Vector3 m_EulerAngleVelocity = new Vector3(0, cabRotateSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        _rb.MoveRotation(_rb.rotation * deltaRotation);
    */
    }

    public void CabTurnLeft()
    {
        _rb.AddTorque(-Vector3.up * cabRotateSpeed);
        /*
        Vector3 m_EulerAngleVelocity = new Vector3(0, -cabRotateSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        _rb.MoveRotation(_rb.rotation * deltaRotation);
    */
    }

    public void CabStationary()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
