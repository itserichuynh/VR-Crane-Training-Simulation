using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger3 : Singleton<TargetTrigger3>
{
    public bool detected = false;

    private void OnTriggerEnter(Collider other)
    {
        detected = true;
    }
}
