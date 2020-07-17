using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR.Interaction.Toolkit;

public class AudioController : Singleton<AudioController>
{

    public GameController gameController;
    public AudioMixerSnapshot cabAmbient;
    public AudioMixerSnapshot startAmbient;
    public AudioSource cabEngineSource;
    public AudioSource cabRadioSource;
    public AudioClip engineStart;
    public AudioClip engineRunning;
    public AudioClip engineEnd;
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
        StartCoroutine(WaitToSwitchOutClips());
    }

    public void CraneEnd()
    {
        /*cabEngineSource.PlayOneShot(engineEnd);*/ // TODO Kirk - need to fix this with shutoff sound
    }

    IEnumerator WaitToSwitchOutClips()
    {
        yield return new WaitForSeconds(engineStart.length);
        cabEngineSource.clip = engineRunning;
        cabEngineSource.loop = true;
        cabEngineSource.Play();
    }
}
