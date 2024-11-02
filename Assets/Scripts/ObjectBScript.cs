using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBScript : MonoBehaviour
{
    public int score = 10;
    private AudioSource SeGet;
    private bool isInitialized = false;

    void Start()
    {
        ScoreCalculator.Instance.AddToCurrentScore(score);
        ScoreCalculator.Instance.AddToTotalScore(score);
        isInitialized = true;
        SeGet = GameObject.Find("Sound").transform.Find("CutSoundEffect").GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        if (isInitialized)
        {
            ScoreCalculator.Instance.AddToCurrentScore(score);
            ScoreCalculator.Instance.AddToTotalScore(score);
        }
    }

    public void OnEliminate()
    {
        //Debug.Log("ObjectB eliminated!");
        //AudioSource.PlayClipAtPoint(SeGet, transform.position);
        SeGet.Play();
    }

}