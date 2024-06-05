using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score { get; private set; } = 0;

    private void OnEnable() 
    {
        GameManager.UpdateScore += updateScore;
        DDOL_Manager.DontDestroyOnLoad(gameObject);
    }

    private void updateScore(float maxHealth, float timeToKill)
    {
        score += (int)MathF.Truncate(maxHealth + 500 / timeToKill - 1);
        scoreText.text = scoreText.text = $"Score: \n{score}";
    }
    private void OnDisable() 
    {
        GameManager.UpdateScore -= updateScore;
    } 
}
