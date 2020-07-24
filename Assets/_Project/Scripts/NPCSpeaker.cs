using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpeaker : MonoBehaviour
{
    public static NPCSpeaker Instance;
    
    public AudioSource audioSource;
    
    public enum Phrase
    {
        Welcome,
        LookAround,
        LastAudio
    }

    [SerializeField] private AudioClip enWelcome;
    [SerializeField] private AudioClip enLookAround;
    [SerializeField] private AudioClip enLastAudio;

    [SerializeField] private AudioClip esWelcome;
    [SerializeField] private AudioClip esLookAround;
    [SerializeField] private AudioClip esLastAudio;

    private AudioClip _welcomeClip;
    private AudioClip _lookAroundClip;
    private AudioClip _lastAudioClip;
    private void Awake()
    {
        if (Instance != null) return;

        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public float Play(Phrase phrase)
    {
        AudioClip clip;

        switch (phrase)
        {
            case Phrase.Welcome:
                clip = _welcomeClip;
                break;
            case Phrase.LookAround:
                clip = _lookAroundClip;
                break;
            case Phrase.LastAudio:
                clip = _lastAudioClip;
                break;
            default:
                clip = null;
                break;
        }
        
        audioSource.clip = clip;
        audioSource.Play();
        
        return clip != null? clip.length : 0;
    }

    public void RepopulateAudioClips()
    {
        switch (LanguageController.CurrentLanguage)
        {
            case LanguageController.EnglishTag:
                _welcomeClip = enWelcome;
                _lookAroundClip = enLookAround;
                _lastAudioClip = enLastAudio;
                break;
            case LanguageController.SpanishTag:
                _welcomeClip = esWelcome;
                _lookAroundClip = esLookAround;
                _lastAudioClip = esLastAudio;
                break;
        }
    }
}
