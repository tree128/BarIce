using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private int currentScore = 0;
    private int totalScore = 0;

    public static ScoreCalculator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public void AddToCurrentScore(int score)
    {
        currentScore += score;
    }

    public void AddToTotalScore(int score)
    {
        totalScore += score;
    }

    public void SubtractFromCurrentScore(int score)
    {
        currentScore -= score;
    }

    public void Multiply(float f)
    {
        currentScore = (int)(currentScore * f);
    }

    public void ScoreInit()
    {
        currentScore = 0;
        totalScore = 0;
    }
    public void ScoreInit_Crash()
    {
        currentScore = 0;
    }
}