using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    public static string CurrentLanguage { get; private set; }
    public const string EnglishTag = "Language EN";
    public const string SpanishTag = "Language ES";
    
    private const string DefaultLanguage = EnglishTag;

    private GameObject[] _englishLabels;
    private GameObject[] _spanishLabels;

    private void Awake()
    {
        LoadObjectsWithLanguageTag();

        Reset();

        ChangeLanguage(DefaultLanguage);
    }

    public void ChangeToSpanish()
    {
        ChangeLanguage(SpanishTag);
    }

    public void ChangeToEnglish()
    {
        ChangeLanguage(EnglishTag);
    }
    
    private void ChangeLanguage(string spokenLanguage)
    {
        GameObject[] labels;
        CurrentLanguage = spokenLanguage;

        switch (spokenLanguage)
        {
            case EnglishTag:
                labels = _englishLabels;
                break;
            case SpanishTag:
                labels = _spanishLabels;
                break;
            default:
                labels = new GameObject[1];
                break;
        }
        
        Reset();
        
        foreach (var label in labels)
        {
            label.SetActive(true);
        }
        
        NPCSpeaker.Instance.RepopulateAudioClips();
    }

    private void LoadObjectsWithLanguageTag()
    {
        _englishLabels =  GameObject.FindGameObjectsWithTag(EnglishTag);
        _spanishLabels =  GameObject.FindGameObjectsWithTag(SpanishTag);
    }

    private void Reset()
    {
        foreach (var label in _englishLabels)
        {
            label.SetActive(false);
        }
        foreach (var label in _spanishLabels)
        {
            label.SetActive(false);
        }
    }
}
