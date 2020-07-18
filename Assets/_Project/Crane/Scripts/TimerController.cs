using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : Singleton<TimerController>
{
    public Text timerText;

    private bool timeStart;
    private float startTime;

    private void Update()
    {
        if (timeStart)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;
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

