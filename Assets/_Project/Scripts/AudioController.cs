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

   
    /*
    // start Flampeyeiry's AudioController content
    private void Awake()
    {
        NPCSpeaker.Instance.Play(NPCSpeaker.Phrase.Welcome);
    }

    public void PlayWelcome()
    {
        NPCSpeaker.Instance.Play(NPCSpeaker.Phrase.Welcome);
    }
    public void PlayLookAround()
    {
        NPCSpeaker.Instance.Play(NPCSpeaker.Phrase.LookAround);
    }
    public void PlayLastAudio()
    {
        NPCSpeaker.Instance.Play(NPCSpeaker.Phrase.LastAudio);
    }
    // end Flampeyeiry's AudioController content
    */
    
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

    public void CraneStart()
    {
        cabEngineSource.PlayOneShot(engineStart);
        StartCoroutine(WaitCraneStart());
    }

    public void CraneEnd()
    {
        cabEngineSource.PlayOneShot(engineEnd);
        // TODO wait until after clip finishes before exiting 
        
    }

    public void AudioCraneIdle()
    {
        if (IsCraneEngineIdle == true)
        {
            craneEngineIdle.volume = 1f;    
        }
        else
        {
            craneEngineIdle.volume = 0f;
        }
    }

    public void AudioCraneRotate()
    {
        if (IsCraneRotate == true)
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
        if (IsCraneHydraulic == true)
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
        if (IsCraneWinch == true)
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
        if (IsCraneRotate == false)
        {
            if (IsCraneHydraulic == false)
            {
                if (IsCraneWinch == false)
                {
                    craneBeep.volume = 0f;
                }
            }
        }
        else
        {
            craneBeep.volume = 1f;
        }
    }


    
    IEnumerator WaitCraneStart()
    {
        yield return new WaitForSeconds(engineStart.length);
        /*
        cabEngineSource.clip = engineRunning;
        cabEngineSource.loop = true;
        cabEngineSource.Play();
        */

    }
}
