using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text killText;
    [SerializeField] TMP_Text scoreText; 
    [SerializeField] TMP_Text timerText;
    [SerializeField] GameObject WinHolder;

    [Header("Exit parameters")]
    [SerializeField] GameObject closedDoor;
    [SerializeField] GameObject openedDoor;
    [SerializeField] GameObject doorCollider;
   
    [Header("BossFight")]
    [SerializeField] GameObject ExitBlockCollider;
    [SerializeField] GameObject BossCollider;
    [SerializeField] Transform bossSpawnPoint;
    [SerializeField] GameObject boss;

    uint KillCounter = 0;
    float timeToKill = 0;
    AudioManager audioManager;
    LevelLoader levelLoader;
    Timer timer;
    Score score;
    
    public static event Action<float,float> UpdateScore;
    private void Awake()
    {
        if (openedDoor || closedDoor)
        {
            openedDoor.SetActive(false);
            closedDoor.SetActive(true);
        }
        EventManager.PlayerSpawned += PlayerSpawned;
        EventManager.EnemySpawned += EnemySpawned;
        EventManager.PlayerDead += PlayerDead;
        EventManager.EnemyDead += EnemyDead;
        ExitCollider.ExitEntered += OnExitEntered;
        BossTrigger.ArenaEntered += OnArenaEntered;
        EventManager.GameEnd += OnGameEnded;
        audioManager = FindObjectOfType<AudioManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
        timer = FindObjectOfType<Timer>();
        score = FindObjectOfType<Score>();
        timer.timerText = timerText;
        score.scoreText = scoreText;
        UpdateScore?.Invoke(0,500);
    }

    private void OnArenaEntered()
    {
        audioManager.StopAll();
        audioManager.Play("BossFight");
        if (boss != null)
        {
            GameObject bossCopy = Instantiate(boss, bossSpawnPoint.position, Quaternion.identity);
        }
        ExitBlockCollider.SetActive(true);
    }

    private void OnGameEnded()
    {
        audioManager.StopAll();
        killText.enabled = false;
        scoreText.enabled = false;
        timerText.enabled = false;
        timer.enabled = false;
        score.enabled = false;
        Destroy(timer.gameObject);
        Destroy(score.gameObject);
        Time.timeScale = 0f;
        WinHolder.SetActive(true);
        audioManager.Play("win");
        int ScoreRecord = PlayerPrefs.GetInt("BestScore");
        if (ScoreRecord < score.score)
            PlayerPrefs.SetInt("BestScore", score.score);
        float TimeRecord = PlayerPrefs.GetFloat("BestTime");
        if (TimeRecord > timer.timeToDisplay)
            PlayerPrefs.SetFloat("BestTime", timer.timeToDisplay);
        GameObject.FindWithTag("Player").SetActive(false);
    }

    private void OnExitEntered()
    {
        levelLoader.LoadNextLevel();
    }

    private void Start()
    {
        audioManager.Play("MainEffect");
        audioManager.Play("Theme");
    }
    public void EnemySpawned(GameObject enemy)
    {
        KillCounter++;
    }
    public void PlayerSpawned(GameObject player)
    {
        Camera.main.GetComponent<CameraComponent>().SetTargerToFollow(player);
        killText.text = $"Enemy left: \n{KillCounter}";
        EventManager.OnTimerStart();
    }

    private void PlayerDead(GameObject player)
    {
        audioManager.StopAll();
        timer.enabled = false; 
        score.enabled = false;
        Destroy(timer);
        Destroy(score);
        levelLoader.LoadFirstLevel();
    }
    void EnemyDead(GameObject obj)
    {
        UpdateScore?.Invoke(obj.GetComponent<HealthComponent>().maxHealth, timeToKill);
        timeToKill = 0;
        Destroy(obj);
        KillCounter--;
        killText.text = $"Enemy left: \n{KillCounter}";
        if (KillCounter == 0)
        {
            OpenDoor();
        }
    }
    private void OpenDoor()
    {
        if (openedDoor || closedDoor)
        {
            openedDoor.SetActive(true);
            closedDoor.SetActive(false);
        }
    }
    private void Update()
    {
        timeToKill += Time.deltaTime;
    }

    private void OnDestroy()
    {
        EventManager.PlayerSpawned -= PlayerSpawned;
        EventManager.EnemySpawned -= EnemySpawned;
        EventManager.PlayerDead -= PlayerDead;
        EventManager.EnemyDead -= EnemyDead;
        ExitCollider.ExitEntered -= OnExitEntered;
        BossTrigger.ArenaEntered -= OnArenaEntered;
        EventManager.GameEnd -= OnGameEnded;
        EventManager.GameEnd -= OnGameEnded;
    }

}
