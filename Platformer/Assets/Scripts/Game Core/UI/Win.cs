using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Win : MonoBehaviour
{
    public static bool bGamePaused = false;
    [SerializeField] GameObject winUI;
    [SerializeField] TMP_Text TimeTitle;
    [SerializeField] TMP_Text ScoreTitle;
    AudioManager audioManager;
    LevelLoader levelLoader;
    Timer time;
    Score score;
    Pause pause;
    private void Awake()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    public void BackToMenu()
    {
        FindObjectOfType<Pause>().enabled = false;
        levelLoader.LoadMenu();
        winUI.SetActive(false);
    }
    // Update is called once per frame
    private void OnEnable()
    {
        score = FindObjectOfType<Score>();
        time = FindObjectOfType<Timer>();
        Debug.Log(time.timeToDisplay);
        TimeSpan timeSpan = TimeSpan.FromSeconds(time.timeToDisplay);
        TimeTitle.text = "Your time: " + timeSpan.ToString(@"mm\:ss\:ff");
        ScoreTitle.text = "Your score: "+ score.score;
    }

}
