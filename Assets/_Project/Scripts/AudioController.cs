using System;
using System.Collections;
using System.Collections.Generic;
using OVR;
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
    public AudioSource sfxTarget;

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

    public void sfxComplete()
    {
        sfxTarget.Play();
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
            craneRotation.volume = .8f;
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
            craneHydraulic.volume = .8f;    
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
            craneWinch.volume = .8f;
        }
        else
        {
            craneWinch.volume = 0f;
        }
    }

    public void AudioCraneBeep()
    {
        /*if (CabRotation.Instance.cabIsTurning || (BoomRaise.Instance.boomIsRaising || ExtendBoom.Instance.boomIsExtending) || HookController.Instance.hookIsMoving)*/
        if (CabRotation.Instance.cabIsTurning)
        {
             craneBeep.volume = .5f;
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
        yield return new WaitForSeconds(engineStart.length - 1f);
        StartCoroutine(FadeIn(craneEngineIdle, 1f));
        /*craneEngineIdle.volume = .8f;*/
    }

    IEnumerator WaitCraneEngineEnd()
    {
        yield return new WaitForSeconds(engineEnd.length - 1f);
        
        GameController.Instance.GotoStart();
    }

    IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}