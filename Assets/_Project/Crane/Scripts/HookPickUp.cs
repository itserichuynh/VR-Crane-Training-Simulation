using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPickUp : MonoBehaviour
{
    public KeyCode Drop_keyA;
    public KeyCode Drop_keyA_B;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cargo") && collision.gameObject.GetComponent<Rigidbody>()) 
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = collision.rigidbody;
        }
        
    }

    private void Update()
    {
        if (Input.GetKey(Drop_keyA) && Input.GetKey(Drop_keyA_B))
        {
            Destroy(gameObject.GetComponent<FixedJoint>());
        }
    }


}
