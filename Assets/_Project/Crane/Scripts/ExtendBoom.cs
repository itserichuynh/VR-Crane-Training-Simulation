using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendBoom : Singleton<ExtendBoom>
{
    [SerializeField]
    GameObject bigBoom;

    Rigidbody _rigidBig;
    Rigidbody _rigid;

    public float extendSpeed = 0.3f;

    public KeyCode Boom_Extend_keyA;
    public KeyCode Boom_Extend_keyA_B;
    public KeyCode Boom_Shorten_keyB;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigidBig = bigBoom.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    { 
        if (Input.GetKey(Boom_Extend_keyA) && Input.GetKey(Boom_Extend_keyA_B))
        {
            BoomExtend();
        }
        else if (Input.GetKey(Boom_Shorten_keyB) && Input.GetKey(Boom_Extend_keyA_B))
        {
            BoomShorten();
        }
        else // to be stationary
        {
            _rigid.velocity = Vector3.zero;
            _rigid.angularVelocity = Vector3.zero;
        }
    }

    public void BoomExtend()
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigidBig.constraints = RigidbodyConstraints.FreezeRotationX;
        _rigid.AddForce(transform.forward * extendSpeed);
    }

    public void BoomShorten()
    {
        _rigid.constraints = RigidbodyConstraints.None;
        _rigidBig.constraints = RigidbodyConstraints.FreezeRotationX;
        _rigid.AddForce(-transform.forward * extendSpeed);
    }
}
