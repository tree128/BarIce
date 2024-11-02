using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCScript : MonoBehaviour
{
    public int initialScore = 10;
    public int extraScoreOnEliminate = 5;

    private SpriteRenderer spriteRenderer;
    private AudioSource SeGet;

    private bool isInitialized = false;

    void Start()
    {
        gameObject.tag = "C";

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        ScoreCalculator.Instance.AddToCurrentScore(initialScore);
        ScoreCalculator.Instance.AddToTotalScore(initialScore);
        isInitialized = true;

        SeGet = GameObject.Find("Sound").transform.Find("CutSoundEffect").GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (isInitialized)
        {
            ScoreCalculator.Instance.AddToCurrentScore(initialScore);
            ScoreCalculator.Instance.AddToTotalScore(initialScore);
        }
    }

    public void OnEliminate()
    {
        //Debug.Log("ObjectC eliminated!");


        ScoreCalculator.Instance.SubtractFromCurrentScore(extraScoreOnEliminate);

        ScoreCalculator.Instance.SubtractFromCurrentScore(initialScore);

        //AudioSource.PlayClipAtPoint(SeGet, transform.position);
        SeGet.Play();
    }
}
