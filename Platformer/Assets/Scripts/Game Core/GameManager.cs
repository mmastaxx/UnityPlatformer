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
    [SerializeField] GameObject closedDoor;
    [SerializeField] GameObject openedDoor;
    [SerializeField] GameObject backDoor;
    uint KillCounter = 0;
    AudioManager audioManager;
    private void Awake()
    {
        openedDoor.SetActive(false);
        backDoor.SetActive(false);
        EventManager.PlayerSpawned += PlayerSpawned;
        EventManager.EnemySpawned += EnemySpawned;
        EventManager.PlayerDead += PlayerDead;
        EventManager.EnemyDead += EnemyDead;
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void EnemySpawned(GameObject enemy)
    {
        KillCounter++;
    }
    public void PlayerSpawned(GameObject player)
    {
        Camera.main.GetComponent<CameraComponent>().SetTargerToFollow(player);
        audioManager.Play("Rain");
        audioManager.Play("ForestTheme");
        UpdateText();
        EventManager.OnTimerStart();
    }

    private void PlayerDead(GameObject player)
    {
        audioManager.StopAll();
        SceneManager.LoadScene("ForestLevel");
    }
    void EnemyDead(GameObject obj)
    {
        Destroy(obj);
        KillCounter--;
        if (KillCounter == 0) 
        {
            OpenDoor();
        }
        CalculatePoints();
        UpdateText();
    }
    private void CalculatePoints()
    {

    }

    private void UpdateText()
    {

        killText.text = $"Enemy left: \n{KillCounter}";
    }
    private void OpenDoor()
    {
        openedDoor.SetActive(true);
        backDoor.SetActive(true);
    }
}
