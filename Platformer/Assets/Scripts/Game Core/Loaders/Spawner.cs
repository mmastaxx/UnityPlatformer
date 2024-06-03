using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform enemySpawnPoints;
    [SerializeField] GameObject[] enemies;
    [SerializeField] Transform playerSpawnPoint;
    [SerializeField] GameObject player;
    

    private void Start()
    {
        for (int i = 0; i< enemySpawnPoints.childCount;i++)
        {
            GameObject enemy = enemies[UnityEngine.Random.Range(0, enemies.Length)];
            GameObject enemyCopy = Instantiate(enemy, enemySpawnPoints.GetChild(i).position + new Vector3(0, (enemy.GetComponent<BoxCollider2D>().size.y * 2.2f)), Quaternion.identity);
            EventManager.OnEnemySpawned(enemyCopy);
        }
        GameObject playerCopy = Instantiate(player, playerSpawnPoint.position, Quaternion.identity);
        EventManager.OnPlayerSpawned(playerCopy);
    }
}
