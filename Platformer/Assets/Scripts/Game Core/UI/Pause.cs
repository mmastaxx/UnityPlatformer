using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool bGamePaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider effectsSlider;
    AudioManager audioManager;
    LevelLoader levelLoader;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    public void MusicVolumeChanged()
    {
        audioManager.SetMusicVolume(musicSlider.value);
    }
    public void EffectsVolumeChanged()
    {
        audioManager.SetEffectsVolume(effectsSlider.value);
    }
    public void BackToMenu()
    {
        resume();
        levelLoader.LoadMenu();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(bGamePaused) 
            {
                resume();
            }
            else 
            {
                pause();
            }
        }
    }

    void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        bGamePaused = true;
    }

    void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        bGamePaused = false;
    }
}
