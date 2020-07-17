using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomRaise : Singleton<BoomRaise>
{
    [SerializeField]
    Transform cylinderA;

    [SerializeField]
    Transform cylinderB;

    Rigidbody _rigid;
    public Vector3 rotation;
    public bool boomIsRaising;
    public float raiseSpeed;
                 
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        raiseSpeed = 25f;
    }

    private void FixedUpdate()
    {
        rotation = transform.localEulerAngles;
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
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
        //_rigidSmall.constraints = RigidbodyConstraints.FreezePositionZ;
        _rigid.AddForce(transform.up * raiseSpeed);
        //Vector3 m_EulerAngleVelocity = new Vector3(raiseSpeed, 0, 0);
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        //_rigid.MoveRotation(_rigid.rotation * deltaRotation);
    }

    public void LowerBoom()
    {
        _rigid.constraints = RigidbodyConstraints.None;
        //_rigidSmall.constraints = RigidbodyConstraints.FreezePositionZ;
        _rigid.AddForce(-transform.up * raiseSpeed);
        //Vector3 m_EulerAngleVelocity = new Vector3(-raiseSpeed, 0, 0);
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        //_rigid.MoveRotation(_rigid.rotation * deltaRotation);
    }

    public void BoomStationary()
    {
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = Vector3.zero;
    }
}
