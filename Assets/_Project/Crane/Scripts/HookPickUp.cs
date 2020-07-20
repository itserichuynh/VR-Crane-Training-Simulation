using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HookPickUp : MonoBehaviour
{
    public TextMeshProUGUI dropText;
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
            cargoDetected = false;
            dropText.text = "Drop";
        }
        else
        {
            dropText.text = "Pick Up";
            Destroy(gameObject.GetComponent<FixedJoint>());
        }
    }


}
