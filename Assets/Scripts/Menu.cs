using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Canvas MenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        MenuCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuCanvas.enabled)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void MenuVisible()
    {
        MenuCanvas.enabled = true;
    }

    public void MenuInvisible()
    {
        MenuCanvas.enabled = false;
        PlayerPrefs.Save();
    }
}
