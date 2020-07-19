using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffButtonController : Singleton<OnOffButtonController>
{
    Renderer _renderer;   

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void ChangeToYellow()
    {
        _renderer.material.color = new Color(0.6f, 0.61f, 0.341f);
    }

    public void ChangeToBlue()
    {
        _renderer.material.color = new Color(0.431f, 0.0745f, 0.416f);
    }
}
