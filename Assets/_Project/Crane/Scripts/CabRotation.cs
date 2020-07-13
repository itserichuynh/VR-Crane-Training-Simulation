using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabRotation : Singleton<CabRotation>
{
    public float cabRotateSpeed = 120f;

    private Rigidbody _rb;

    public KeyCode Cab_Right_keyA;
    public KeyCode Cab_keyA_B;
    public KeyCode Cab_Left_keyB;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(Cab_Right_keyA) && Input.GetKey(Cab_keyA_B))
        {
            _rb.AddTorque(Vector3.up * cabRotateSpeed);
        }
        else if (Input.GetKey(Cab_Left_keyB) && Input.GetKey(Cab_keyA_B))
        {
            _rb.AddTorque(-Vector3.up * cabRotateSpeed);
        }      
    }
}
