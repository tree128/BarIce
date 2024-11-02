using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Crush : MonoBehaviour
{
    [SerializeField] private GameObject crushIceRootObj;
    [SerializeField] private CrushIceRoot crushIceRoot;
    [SerializeField] private StageManager stageManager;
    [SerializeField] private GameObject cutterObj;
    [SerializeField] private PicCutterManager cutter;

    // Start is called before the first frame update
    void Start()
    {
        /*crushIceRootObj = GameObject.Find("CrushIceRoot");
        crushIceRoot = crushIceRootObj.GetComponent<CrushIceRoot>();
        cutterObj = crushIceRoot.Cutter;
        cutter = cutterObj.GetComponentInParent<PicCutterManager>();
        stageManager = crushIceRoot.StageManager;*/
        crushIceRoot = GameObject.Find("CrushIceRoot").GetComponent<CrushIceRoot>();
        cutter = GameObject.Find("Pic&CutterManager").GetComponent<PicCutterManager>();
        cutterObj = GameObject.Find("Cutter(Clone)");
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
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
            
            else if (collision.gameObject.CompareTag("B"))
            {
                Debug.Log("Object has tag B!"); 
                collision.gameObject.GetComponent<ObjectBScript>().OnEliminate();
            }
            
            else if (gameObject.CompareTag("C"))
            {
                gameObject.GetComponent<ObjectCScript>().OnEliminate();
            }
            
            cutter.PlayCutEffect();
            Destroy(gameObject);
        }

    }
}
