using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using UnityEditor.Experimental.GraphView;
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

    private float score;
    
    private static string spokenLanguage = "en";
    public bool engineRunning = false;

    private void FixedUpdate()
    {
        score = ScoreController.Instance.scoretotal
            + ScoreController1.Instance.scoretotal
            + ScoreController2.Instance.scoretotal
            + ScoreController3.Instance.scoretotal
            + ScoreController4.Instance.scoretotal
            + ScoreController5.Instance.scoretotal
            + ScoreController6.Instance.scoretotal
            + ScoreController7.Instance.scoretotal
            + ScoreController8.Instance.scoretotal;
        scoreText.text = "Score: " + score.ToString();
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
    }

    public void LanguageEN()
    {
        spokenLanguage = "en";
        Debug.Log("selected language english");
    }

    public void LanguageES()
    {
        spokenLanguage = "es";
        Debug.Log("selected language espanol");
    }

    public void Engine()
    {
        if (engineRunning == false)
        {
            audioController.CraneStart();
            engineRunning = true;
            // TODO Kirk - change button material to engine_on

            offText.gameObject.SetActive(false); // off text disappears
            onText.gameObject.SetActive(true); // on text appears
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

            offText.gameObject.SetActive(true); // off text appears
            onText.gameObject.SetActive(false); // on text dosappears
            OnOffButtonController.Instance.ChangeToBlue(); // Change color to yellow (OFF)
            TimerController.Instance.StopTimer(); // Stop timer
        }
    }    
}

