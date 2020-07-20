using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger1 : Singleton<TargetTrigger1>
{
    public bool detected = false;

    private void OnTriggerEnter(Collider other)
    {
        detected = true;
    }
}
