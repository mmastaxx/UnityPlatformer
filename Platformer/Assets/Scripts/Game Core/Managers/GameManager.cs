using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMP_Text killText;
    [SerializeField] TMP_Text scoreText;

    [Header("Exit parameters")]
    [SerializeField] GameObject closedDoor;
    [SerializeField] GameObject openedDoor;
    [SerializeField] GameObject doorCollider;
    uint Score = 0;
    uint KillCounter = 0;
    float timeToKill = 0;
    AudioManager audioManager;
    LevelLoader levelLoader;
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
        audioManager = FindObjectOfType<AudioManager>();
        levelLoader = FindObjectOfType<LevelLoader>();
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
        UpdateText();
        EventManager.OnTimerStart();
    }

    private void PlayerDead(GameObject player)
    {
        audioManager.StopAll();
        levelLoader.LoadFirstLevel();
    }
    void EnemyDead(GameObject obj)
    {
        CalculatePoints(obj.GetComponent<HealthComponent>().maxHealth);
        timeToKill = 0;
        Destroy(obj);
        KillCounter--;
        if (KillCounter == 0)
        {
            OpenDoor();
        }
        UpdateText();
    }
    private void CalculatePoints(float maxHealth)
    {
        Score += (uint)MathF.Truncate(maxHealth + 500 / timeToKill);
    }

    private void UpdateText()
    {
        scoreText.text = $"Score: \n{Score}";
        killText.text = $"Enemy left: \n{KillCounter}";
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
    }
   
}
