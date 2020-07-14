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
    
    IEnumerator WaitToSwitchOutClips(){
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = engineRunning;
        audioSource.loop = true (or enabled I forget);
        audioSource.Play();
    }
}
