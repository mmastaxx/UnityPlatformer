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
    [SerializeField] Camera Camera;
    [SerializeField] Spawner spawner;
    [SerializeField] TextMeshProUGUI killText;
    uint KillCounter = 0;
    private void Awake()
    {
        Debug.Log("GameManager");
        spawner.OnPlayerSpawned += PlayerSpawned;
        spawner.OnEnemySpawned += EnemySpawned;
    }
    public void EnemySpawned(GameObject enemy)
    {
        enemy.GetComponent<HealthComponent>().OnDead += EnemyDead;
        KillCounter++;
    }
    public void PlayerSpawned(GameObject player)
    {
        Debug.Log("PlayerSpawned");
        player.GetComponent<HealthComponent>().OnDead += PlayerDead;
        Camera.GetComponent<CameraComponent>().playerTransform = player.GetComponent<Transform>();
        UpdateText();
    }

    private void PlayerDead(GameObject player)
    {
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

    

    // Update is called once per frame
    void Update()
    {

    }


    private void CalculatePoints()
    {

    }

    private void UpdateText()
    {
        killText.text = $"Enemy killed: \n{KillCounter}";
    }
    private void OpenDoor()
    {

    }
}
