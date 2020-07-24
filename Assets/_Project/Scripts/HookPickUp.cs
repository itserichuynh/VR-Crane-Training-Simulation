using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HookPickUp : Singleton<HookPickUp>
{
    public bool cargoDetected = false;
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
            cargoDetected = false;
        }
        else
        {
            Destroy(gameObject.GetComponent<FixedJoint>());
            /*cargoDetected = false;*/
        }
    }


}
