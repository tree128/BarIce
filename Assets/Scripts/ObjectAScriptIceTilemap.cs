using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAScriptTilemap : MonoBehaviour
{
    public int score = 10;
    private bool isInitialized = false;

    void Start()
    {
        if (!isInitialized)
        {
            ScoreCalculator.Instance.AddToTotalScore(score);
            isInitialized = true;
        }
    }

    public void OnEliminate()
    {
        if (!isInitialized)
        {
            ScoreCalculator.Instance.AddToTotalScore(score);
            isInitialized = true;
        }

        ScoreCalculator.Instance.AddToCurrentScore(score);

    }
}