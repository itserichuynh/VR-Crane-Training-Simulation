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
    public void ChangeToRed()
    {
        _renderer.material.color = Color.red;
    }

    public void ChangeToGreen()
    {
        _renderer.material.color = Color.green;
    }
}
