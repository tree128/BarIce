using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutClick : MonoBehaviour
{
    public AudioSource CutSE;

    private CutIce cutIce;
    private ParticleSystem point;
    private BoxCollider2D pointCollider;
    private int clickedCount;
    private int targetValue = 2;
    private string methodName = "True_isCut";
    private string methodNum;

    // Start is called before the first frame update
    void Start()
    {
        cutIce = GetComponentInParent<CutIce>();
        point = gameObject.GetComponentInChildren<ParticleSystem>();
        pointCollider = point.GetComponentInChildren<BoxCollider2D>();
        methodNum = gameObject.name.Substring(gameObject.name.Length - 1);
        methodName += methodNum;
        Init();        
    }

    // Update is called once per frame
    void Update()
    {
        if(clickedCount == targetValue)
        {
            clickedCount = 0;
            point.gameObject.SetActive(false);
            var method = cutIce.GetType().GetMethod(methodName);
            method.Invoke(cutIce, null);
        }
    }

    public void ClickCount()
    {
        clickedCount++;
        CutSE.Play();
    }

    public void Init()
    {
        clickedCount = 0;
        if (methodNum == "1" || methodNum == "3")
        {
            point.gameObject.SetActive(true);
            float randomNum = Random.Range(-0.4f, 0.4f);
            point.gameObject.transform.localPosition = new Vector3(0f, randomNum, 0f);
            pointCollider.size = new Vector2(1f, 0.5f);
        }
        else if (methodNum == "2" || methodNum == "4")
        {
            point.gameObject.SetActive(true);
            float randomNum = Random.Range(-0.4f, 0.4f);
            point.gameObject.transform.localPosition = new Vector3(randomNum, 0f, 0f);
            pointCollider.size = new Vector2(0.5f, 1f);
        }
        else
        {
            point.gameObject.SetActive(false);
            point.gameObject.transform.localPosition = Vector3.zero;
        }
    }
}
