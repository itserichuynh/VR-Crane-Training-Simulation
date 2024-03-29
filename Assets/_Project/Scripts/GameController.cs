﻿using System;
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

    private bool EngineButtonInteractable = true;
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
        if (!EngineButtonInteractable) return;
        
        if (engineRunning == false)
        {
            engineRunning = true;
            audioController.AudioCraneEngineIdle();
            OnOffButtonController.Instance.ChangeToYellow(); // Change color to yellow (ON)
            TimerController.Instance.StartTimer(); // Start timer
        }
        else
        {
            engineRunning = false;
            audioController.AudioCraneEngineIdle();
            
            // GotoStart(); - this is called in AudioController on engine shutoff

            OnOffButtonController.Instance.ChangeToWhite(); // Change color to white (OFF)
            TimerController.Instance.StopTimer(); // Stop timer
        }

        StartCoroutine(TempDisableEngineButton());
    }

    void Start()
    {
        npc.transform.LookAt(xrRigLocation);
    }

    private IEnumerator TempDisableEngineButton()
    {
        EngineButtonInteractable = false;
        yield return new WaitForSeconds(5f);
        EngineButtonInteractable = true;
    }
}

