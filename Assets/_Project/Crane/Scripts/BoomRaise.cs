using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomRaise : Singleton<BoomRaise>
{
    [SerializeField]
    GameObject smallBoom;

    [SerializeField]
    Transform cylinderA;

    [SerializeField]
    Transform cylinderB;

    Rigidbody _rigid;
    Rigidbody _rigidSmall;
    
    public float raiseSpeed = 25;

    public KeyCode Boom_Raise_keyA;
    public KeyCode Boom_Raise_keyA_B;
    public KeyCode Boom_Lower_keyB;
                 
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigidSmall = smallBoom.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(Boom_Raise_keyA) && Input.GetKey(Boom_Raise_keyA_B))
        {
            RaiseBoom();
        }
        else if(Input.GetKey(Boom_Lower_keyB) && Input.GetKey(Boom_Raise_keyA_B))
        {
            LowerBoom();
        }
        else // to be stationary
        {
            _rigid.velocity = Vector3.zero;
            _rigid.angularVelocity = Vector3.zero;
        }
        
    }

    void LateUpdate()
    {

        if (cylinderA != null && cylinderB != null)
        {

            cylinderA.LookAt(cylinderB.position);
            cylinderB.LookAt(cylinderA.position);
        }
    }

    public void RaiseBoom()
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigidSmall.constraints = RigidbodyConstraints.FreezePositionZ;
        _rigid.AddForce(transform.up * raiseSpeed);
    }

    public void LowerBoom()
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigidSmall.constraints = RigidbodyConstraints.FreezePositionZ;
        _rigid.AddForce(-transform.up * raiseSpeed);
    }
}
