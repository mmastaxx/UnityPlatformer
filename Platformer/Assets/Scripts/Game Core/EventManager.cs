
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

public static class EventManager
{
    public static event UnityAction TimerStart; 
    public static event UnityAction TimerStop;
    public static event UnityAction<GameObject> PlayerSpawned;
    public static event UnityAction<GameObject> EnemySpawned;
    public static event UnityAction<GameObject> PlayerDead;
    public static event UnityAction<GameObject> EnemyDead;
    public static void OnTimerStart() => TimerStart?.Invoke();
    public static void OnTimerStop() => TimerStop?.Invoke();
    public static void OnPlayerSpawned(GameObject obj) => PlayerSpawned?.Invoke(obj);
    public static void OnEnemySpawned(GameObject obj) => EnemySpawned?.Invoke(obj);
    public static void OnPlayerDead(GameObject obj) => PlayerDead?.Invoke(obj);
    public static void OnEnemyDead(GameObject obj) => EnemyDead?.Invoke(obj);

}
