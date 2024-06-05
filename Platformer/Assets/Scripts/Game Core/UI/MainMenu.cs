using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;
using TMPro;


public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject RecordsHadler;
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip soundTrophy;
    [SerializeField] AudioClip soundPlay;
    [SerializeField] private TMP_Text time;
    [SerializeField] private TMP_Text score;
    bool isOpened = false;
    private void Start()
    {
        DDOL_Manager.DestroyAll();
    }
    public void StartGame() 
    {
        soundSource.PlayOneShot(soundPlay);
        SceneManager.LoadScene(gameObject.scene.buildIndex+1);
    }
    public void CheckRecords()
    {
        if (!isOpened)
        {
            isOpened = true;
            soundSource.PlayOneShot(soundTrophy);
            float bestTime = PlayerPrefs.GetFloat("BestTime", 3599.99f);
            TimeSpan timespan = TimeSpan.FromSeconds(bestTime);
            time.text = "Best time " + timespan.ToString(@"mm\:ss\:ff");
            score.text = "Best score " + PlayerPrefs.GetInt("BestScore", 0);
        }
        else
            isOpened = false;
        RecordsHadler.SetActive(isOpened);
    }

    // Update is called once per frame
    public void QuitGame() 
    {
        Application.Quit();
    }
}
