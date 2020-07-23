using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class GameController : Singleton<GameController>

{
    public AudioController audioController;
    public GameObject locationStart;
    public GameObject locationCab;
    public Transform xrRigLocation;
    public GameObject xrRigInCab;
    public Text scoreText;
    public GameObject npc;
    public float score;
    
    private static string spokenLanguage = "en";
    public bool engineRunning = false;

    private void FixedUpdate()
    {
        score = ScoreController.scoretotal;
            
        scoreText.text = score.ToString();
    }

    public void GotoCab()
    {
        xrRigLocation.transform.rotation = xrRigInCab.transform.rotation;
        
        xrRigLocation.position=new Vector3(
            locationCab.transform.position.x,
            locationCab.transform.position.y,
            locationCab.transform.position.z);
        xrRigLocation.transform.SetParent(xrRigInCab.transform);
        audioController.AtCab();
        npc.transform.LookAt(xrRigLocation);
    }

    public void GotoStart()
    {
        xrRigLocation.transform.rotation = Quaternion.Euler(0,0,0);
        
        xrRigLocation.position=new Vector3(
            locationStart.transform.position.x,
            locationStart.transform.position.y,
            locationStart.transform.position.z);
        xrRigLocation.transform.SetParent(null);
        audioController.AtStart();
        npc.transform.LookAt(xrRigLocation);
    }

    public void Engine()
    {
        if (engineRunning == false)
        {
            audioController.CraneStart();
            engineRunning = true;
            // TODO Kirk - change button material to engine_on

            OnOffButtonController.Instance.ChangeToYellow(); // Change color to yellow (ON)
            TimerController.Instance.StartTimer(); // Start timer
        }
        else
        {
            // TODO Kirk - crane shutdown sound & wait for sound to finish
            // TODO Kirk - set variable for completion of task
            // TODO Kirk - change button material to engine_off
            engineRunning = false;
            GotoStart();

            OnOffButtonController.Instance.ChangeToWhite(); // Change color to white (OFF)
            TimerController.Instance.StopTimer(); // Stop timer
        }
    }

    void Start()
    {
        npc.transform.LookAt(xrRigLocation);
    }
}

