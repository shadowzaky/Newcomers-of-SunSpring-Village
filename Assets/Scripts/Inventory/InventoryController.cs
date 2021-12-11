using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject panel;
    public GameObject topPanel;
    public GameObject toolbarPanel;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            panel.SetActive(!panel.activeInHierarchy);
            toolbarPanel.SetActive(!toolbarPanel.activeInHierarchy);
            topPanel.SetActive(!topPanel.activeInHierarchy);
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        toolbarPanel.SetActive(false);
    }

    public void Close()
    {
        panel.SetActive(false);
        toolbarPanel.SetActive(true);
    }
}
