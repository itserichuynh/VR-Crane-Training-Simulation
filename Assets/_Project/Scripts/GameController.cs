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
    public Text offText;
    public Text onText;
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
            
            offText.gameObject.SetActive(false); // off text disappears
            onText.gameObject.SetActive(true); // on text appears
            OnOffButtonController.Instance.ChangeToYellow(); // Change color to yellow (ON)
            TimerController.Instance.StartTimer(); // Start timer

            UIController.Instance.ActivateLevers(); // Activate all levers when the On/Off button is pressed
        }
        else
        {
            engineRunning = false;
            GotoStart();

            offText.gameObject.SetActive(true); // off text appears
            onText.gameObject.SetActive(false); // on text dosappears
            OnOffButtonController.Instance.ChangeToBlue(); // Change color to yellow (OFF)
            TimerController.Instance.StopTimer(); // Stop timer

            UIController.Instance.DisableLevers(); // Disable all levers when the crane if off
        }
    }

    void Start()
    {
        npc.transform.LookAt(xrRigLocation);
    }
}

