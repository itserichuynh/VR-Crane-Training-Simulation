using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropButtonController : MonoBehaviour
{
    Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
    public void ChangeToWhite()
    {
        _renderer.material.color = Color.white;
    }

    public void ChangeToYellow()
    {
        _renderer.material.color = Color.yellow;
    }
}
