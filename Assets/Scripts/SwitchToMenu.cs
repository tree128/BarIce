using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToMenu : MonoBehaviour
{
    public void SwitchToMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}