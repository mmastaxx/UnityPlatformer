using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject Camera;
    [SerializeField] Transform playerSpawn;
    [SerializeField] Transform EnemySpawn;
    void Start()
    {
        Instantiate(spawner);
        Instantiate(gameManager);
        Instantiate(hud);
        Instantiate(Camera);
    }

}
