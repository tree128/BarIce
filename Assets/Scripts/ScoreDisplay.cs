using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text ScoreText;
    public Text GameTotalScoreText;

    [SerializeField] private int currentScore;
    [SerializeField] private int totalScore;
    [SerializeField] private int gameTotalScore = 0;
    [SerializeField] private int beforeTurnScore = 0;

    void Update()
    {
        currentScore = ScoreCalculator.Instance.GetCurrentScore();
        totalScore = ScoreCalculator.Instance.GetTotalScore();
        ScoreText.text = currentScore.ToString() + " / " + totalScore.ToString();
    }

    public void ScoreSave()
    {
        currentScore = ScoreCalculator.Instance.GetCurrentScore();
        gameTotalScore = beforeTurnScore + currentScore;
        GameTotalScoreText.text = "Total:" + gameTotalScore.ToString();
        beforeTurnScore = gameTotalScore;
    }
}