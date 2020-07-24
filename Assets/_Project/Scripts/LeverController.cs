using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    Rigidbody _rigid;
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigid.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        _rigid.isKinematic = false;
    }

    private void OnTriggerExit(Collider other)
    {
        _rigid.isKinematic = true;
    }
}
