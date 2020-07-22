using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    Renderer _renderer;
    public Color colorHit;
    public Color colorMiss;
    
    public static float scoretotal = 0;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cargo"))
        {
            print(gameObject.name + "+1 point");
            scoretotal = scoretotal + 1;
            _renderer.material.color = colorHit;
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cargo"))
        {
            print(gameObject.name + "-1 point");
            scoretotal = scoretotal -1;
            _renderer.material.color = colorMiss;
        }
    }

}
