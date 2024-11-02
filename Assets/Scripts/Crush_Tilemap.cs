using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Crush_Tilemap : MonoBehaviour
{
    private StageManager stageManager;
    private GameObject cutterObj;
    private PicCutterManager_Tilemap cutter;

    // Start is called before the first frame update
    void Start()
    {
        cutter = GameObject.Find("Pic&CutterManager").GetComponent<PicCutterManager_Tilemap>();
        cutterObj = GameObject.Find("CutterImage");
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //For Score System
        if (collision.gameObject == cutterObj && stageManager.CurrentStage == StageManager.Stage.Crush)
        {
            if (gameObject.CompareTag("A"))
            {
                gameObject.GetComponent<ObjectAScript>().OnEliminate();
            }

            else if (gameObject.CompareTag("B"))
            {
                Debug.Log("Object has tag B!");
                gameObject.GetComponent<ObjectBScript>().OnEliminate();
            }

            else if (gameObject.CompareTag("C"))
            {
                gameObject.GetComponent<ObjectCScript>().OnEliminate();
            }

            cutter.PlayCutEffect(gameObject.transform.position);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

    }
}
