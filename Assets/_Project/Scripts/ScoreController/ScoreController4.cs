﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController4 : Singleton<ScoreController4>
{
    public float scoretotal = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cargo"))
        {
            print(gameObject.name + "+1 point");
            scoretotal = scoretotal + 1;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cargo"))
        {
            print(gameObject.name + "-1 point");
            scoretotal = scoretotal - 1;

        }
    }

}