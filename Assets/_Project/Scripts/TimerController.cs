using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : Singleton<TimerController>
{
    public Text timerText;

    private bool timeStart = false;
    private float startTime;

    private void Update()
    {
        if (timeStart)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f0");

            timerText.text = minutes + "m : " + seconds + "s";
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timeStart = true;
    }

    public void StopTimer()
    {
        timeStart = false;
    }
}

