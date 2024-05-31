using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private float timeToDisplay = 0;
    private bool isRunning = false;

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
    }

    private void EventManagerOnTimerStop() { isRunning = false; }

    private void EventManagerOnTimerStart() { isRunning = true; }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
    }
    void Update()
    {
        if (isRunning)
        {
            timeToDisplay += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
            timerText.text = "Time:\n"+timeSpan.ToString(@"mm\:ss\:ff");
        }

    }
}
