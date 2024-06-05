using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;
    public float timeToDisplay{ get; private set; } = 0;
    private bool isRunning = false;

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        DDOL_Manager.DontDestroyOnLoad(gameObject);
    }

    private void EventManagerOnTimerStop() { isRunning = false; }

    private void EventManagerOnTimerStart() { isRunning = true; }
    
    void Update()
    {
        if (isRunning && timerText!=null)
        {
            timeToDisplay += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
            timerText.text = "Time:\n"+timeSpan.ToString(@"mm\:ss\:ff");
        }
    }
    
    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
    }
}
