using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger6 : Singleton<TargetTrigger6>
{
    public bool detected = false;

    private void OnTriggerEnter(Collider other)
    {
        detected = true;
    }
}
