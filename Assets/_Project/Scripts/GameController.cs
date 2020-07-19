using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;

public class GameController : Singleton<GameController>

{
    public AudioController audioController;
    public GameObject locationStart;
    public GameObject locationCab;
    public Transform rigLocation;

    private static string spokenLanguage = "en";
    private bool engineRunning = false;
    
    public void GotoCab()
    {
        /*
        if (gameObject.name.Contains("Hard Hat es"))
        {
            spokenLanguage = "es";
        }
        else
        {
            spokenLanguage = "en";
        }
        */

        rigLocation.position=new Vector3(
            locationCab.transform.position.x,
            locationCab.transform.position.y,
            locationCab.transform.position.z);
        audioController.AtCab();
    }


    public void GotoStart()
    {
        rigLocation.position=new Vector3(
            locationStart.transform.position.x,
            locationStart.transform.position.y,
            locationStart.transform.position.z);
        audioController.AtStart();
    }

    public void Engine()
    {
        if (engineRunning == false)
        {
            audioController.CraneStart();
            engineRunning = true;
            // TODO Kirk - change button material to engine_on
        }
        else
        {
            // TODO Kirk - crane shutdown sound & wait for sound to finish
            // TODO Kirk - set variable for completion of task
            // TODO Kirk - change button material to engine_off
            engineRunning = false;
            GotoStart();
        }
    }

    void Start()
    {
        
    }
    
}

