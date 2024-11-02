using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject[] popupPanels;

    void Start()
    {
        foreach (GameObject panel in popupPanels)
        {
            panel.SetActive(false);
        }
    }

    public void ShowPopup(int index)
    {
        if (index >= 0 && index < popupPanels.Length)
        {
            popupPanels[index].SetActive(true);
        }
    }

    public void ClosePopup(int index)
    {
        if (index >= 0 && index < popupPanels.Length)
        {
            popupPanels[index].SetActive(false);
        }
    }
}