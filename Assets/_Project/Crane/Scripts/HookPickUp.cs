using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPickUp : MonoBehaviour
{
    bool cargoDetected;
    Collision cargo;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cargo") && collision.gameObject.GetComponent<Rigidbody>()) 
        {
            cargoDetected = true;
            cargo = collision;
        }
        
    }
    public void PickUp()
    {
        if (cargoDetected)
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = cargo.rigidbody;
        }        
    }


    public void Drop()
    {
        Destroy(gameObject.GetComponent<FixedJoint>());
        cargoDetected = false;
    }


}
