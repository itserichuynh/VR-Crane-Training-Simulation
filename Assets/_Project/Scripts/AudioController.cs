using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;

public class AudioController : MonoBehaviour
{

    public GameController gameController;
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
    
    public void AtCab()
    {
        cabAmbient.TransitionTo(.1f);    
    }

    public void AtStart()
    {
        startAmbient.TransitionTo(.1f);
    }

    public void Radio()
    {
        cabRadioSource.PlayOneShot(npcWelcome);
    }

    public void CraneStart()
    {
        cabEngineSource.PlayOneShot(engineStart);        
        // TODO get engine running idle sound loop after start plays
    }
}
