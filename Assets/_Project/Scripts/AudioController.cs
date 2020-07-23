using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;

public class AudioController : Singleton<AudioController>
{

    public AudioMixerSnapshot cabAmbient;
    public AudioMixerSnapshot startAmbient;
    public AudioSource cabEngineSource;
    public AudioSource cabRadioSource;

    public AudioSource craneEngineIdle;
    public AudioSource craneBeep;
    public AudioSource craneRotation;
    public AudioSource craneHydraulic;
    public AudioSource craneWinch;
    
    public AudioClip engineStart;
    public AudioClip engineEnd;

    public AudioMixerSnapshot cabRadio;
    public AudioMixerSnapshot outsideSpatialized;
    public AudioSource radioSource;

    public void AtCab()
    {
        cabAmbient.TransitionTo(.1f);
        cabRadio.TransitionTo(.1f);
        radioSource.spatialBlend = 0f;
    }

    public void AtStart()
    {
        startAmbient.TransitionTo(.1f);
        outsideSpatialized.TransitionTo(.1f);
        radioSource.spatialBlend = 1f;
    }

    public void AudioCraneEngineIdle()
    {
        if (GameController.Instance.engineRunning == true)
        {
            cabEngineSource.PlayOneShot(engineStart);
            StartCoroutine(WaitCraneEngineStart());
        }
        else
        {
            craneEngineIdle.volume = 0f;
            cabEngineSource.PlayOneShot(engineEnd);
            StartCoroutine(WaitCraneEngineEnd());
        }
    }

    public void AudioCraneRotate()
    {
        if (CabRotation.Instance.cabIsTurning == true)
        {
            craneRotation.volume = 1f;    
        }
        else
        {
            craneRotation.volume = 0f;
        }
    }

    public void AudioCraneHydraulic()
    {
        if (BoomRaise.Instance.boomIsRaising || ExtendBoom.Instance.boomIsExtending == true)
        {
            craneHydraulic.volume = 1f;    
        }
        else
        {
            craneHydraulic.volume = 0f;
        }
    }

    public void AudioCraneWinch()
    {
        if (HookController.Instance.hookIsMoving == true)
        {
            craneWinch.volume = 1f;
        }
        else
        {
            craneWinch.volume = 0f;
        }
    }

    public void AudioCraneBeep()
    {
        if (CabRotation.Instance.cabIsTurning || (BoomRaise.Instance.boomIsRaising || ExtendBoom.Instance.boomIsExtending) || HookController.Instance.hookIsMoving)
        {
             craneBeep.volume = 1f;
        }
        else
        {
            craneBeep.volume = 0f;
        }
    }

    void Update()
    {
        AudioCraneRotate();
        AudioCraneHydraulic();
        AudioCraneWinch();
        AudioCraneBeep();
    }

    
    IEnumerator WaitCraneEngineStart()
    {
        yield return new WaitForSeconds(engineStart.length);
        craneEngineIdle.volume = 1f;
    }

    IEnumerator WaitCraneEngineEnd()
    {
        yield return new WaitForSeconds(engineEnd.length);
        
        GameController.Instance.GotoStart();
    }
}