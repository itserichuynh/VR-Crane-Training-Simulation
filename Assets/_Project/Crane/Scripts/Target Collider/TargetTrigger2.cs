using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger2 : Singleton<TargetTrigger2>
{
    public bool detected = false;

    private void OnTriggerEnter(Collider other)
    {
        detected = true;
        this.gameObject.SetActive(false);
        AudioController.Instance.sfxTarget.Play();
    }
}
