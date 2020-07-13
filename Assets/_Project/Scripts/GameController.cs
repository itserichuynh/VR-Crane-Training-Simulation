using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;

public class GameController : Singleton<GameController>

{
    public GameObject locationStart;
    public GameObject locationCab;
    public Transform rigLocation;
    public AudioMixer audioInCabMixer;
    public AudioMixer audioInCabRadio;
    public AudioMixer audioInCabEngine;
    public AudioMixerSnapshot cabAmbient;
    public AudioMixerSnapshot startAmbient;
    public AudioMixerSnapshot cabRadio;
    public AudioMixerSnapshot cabEngine;
    public AudioSource cabEngineSource;
    public AudioSource cabRadioSource;
    public AudioClip engineStart;
    public AudioClip engineRunning;
    public AudioClip npcWelcome;
    
    public void GotoCab()
    {
        rigLocation.position=new Vector3(locationCab.transform.position.x,locationCab.transform.position.y,locationCab.transform.position.z);
        cabAmbient.TransitionTo(.1f);    
    }

    public void GotoStart()
    {
        rigLocation.position=new Vector3(locationStart.transform.position.x,locationStart.transform.position.y,locationStart.transform.position.z);
        startAmbient.TransitionTo(.1f);
    }

    public void Radio()
    {
        cabRadioSource.PlayOneShot(npcWelcome);
    }

    public void StartCraneEngine()
    {
        

    }
}

