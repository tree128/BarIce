using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //public GameObject TargetScene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change_Scene()
    {
        //SceneManager.LoadScene(TargetScene.name, LoadSceneMode.Single);
        SceneManager.LoadScene("Bar", LoadSceneMode.Single);
    }
}