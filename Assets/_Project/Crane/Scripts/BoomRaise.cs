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
                 
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigidSmall = smallBoom.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {

        if (cylinderA != null && cylinderB != null)
        {

            cylinderA.LookAt(cylinderB.position);
            cylinderB.LookAt(cylinderA.position);
        }
    }

    public void RaiseBoom(float raiseSpeed)
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigidSmall.constraints = RigidbodyConstraints.FreezePositionZ;
        _rigid.AddForce(transform.up * raiseSpeed);
    }

    public void LowerBoom(float raiseSpeed)
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigidSmall.constraints = RigidbodyConstraints.FreezePositionZ;
        _rigid.AddForce(-transform.up * raiseSpeed);
    }

    public void BoomStationary()
    {
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
    }
}
