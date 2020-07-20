using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger4 : Singleton<TargetTrigger4>
{
    public bool detected = false;

    private void OnTriggerEnter(Collider other)
    {
        detected = true;
    }
}
