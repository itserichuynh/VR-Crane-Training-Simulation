using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPickUp : MonoBehaviour
{
    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cargo") && collision.gameObject.GetComponent<Rigidbody>()) 
        {
            yield return new WaitForSeconds(2f);
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody = collision.rigidbody;
        }
        
    }

    public void Drop()
    {
        Destroy(gameObject.GetComponent<FixedJoint>());
    }


}
