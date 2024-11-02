using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAScript : MonoBehaviour
{
    public int score = 10;
    private bool isInitialized = false;
    private AudioSource SeGet;

    void Start()
    {
        /*if (!isInitialized)
        {
            ScoreCalculator.Instance.AddToTotalScore(score);
            isInitialized = true;
        }*/

        ScoreCalculator.Instance.AddToTotalScore(score);
        isInitialized = true;
        SeGet = GameObject.Find("Sound").transform.Find("CutSoundEffect").GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (isInitialized)
        {
            ScoreCalculator.Instance.AddToTotalScore(score);
        }
    }

    public void OnEliminate()
    {
        /*if (!isInitialized)
        {
            ScoreCalculator.Instance.AddToTotalScore(score);
            isInitialized = true;
        }*/
        //Debug.Log("ObjectA eliminated!");
        ScoreCalculator.Instance.AddToCurrentScore(score);

        //AudioSource.PlayClipAtPoint(SeGet, transform.position);
        SeGet.Play();
    }
}