using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendBoom : Singleton<ExtendBoom>
{
    [SerializeField]
    GameObject bigBoom;

    Rigidbody _rigidBig;
    Rigidbody _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigidBig = bigBoom.GetComponent<Rigidbody>();
    }

    public void BoomExtend(float extendSpeed)
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigidBig.constraints = RigidbodyConstraints.FreezeRotationX;
        _rigid.AddForce(transform.forward * extendSpeed);
    }

    public void BoomShorten(float extendSpeed)
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigidBig.constraints = RigidbodyConstraints.FreezeRotationX;
        _rigid.AddForce(-transform.forward * extendSpeed);
    }

    public void ExtendStationary()
    {
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
    }
}
