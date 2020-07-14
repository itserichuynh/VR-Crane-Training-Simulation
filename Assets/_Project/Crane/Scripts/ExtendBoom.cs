using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendBoom : Singleton<ExtendBoom>
{
    Rigidbody _rigid;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    public void BoomExtend(float extendSpeed)
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigid.AddForce(transform.forward * extendSpeed);
    }

    public void BoomShorten(float extendSpeed)
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigid.AddForce(-transform.forward * extendSpeed);
    }

    public void ExtendStationary()
    {
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
    }
}
