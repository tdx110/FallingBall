using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject gameObjectChoseCanvas;
    [SerializeField]
    private GameObject gameObjectCanvasButton;
    [SerializeField]
    private GameObject gameObjectButtonLeft;
    [SerializeField]
    private GameObject gameObjectButtonRight;
    [SerializeField]
    private GameObject gameObjectCanvasWarring;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }
    void OnDestroy()
    {
        Time.timeScale = 1;
    }

    public void ButtonNo()
    {
        Destroy(gameObject);
    }
    public void ButtonYes()
    {
        gameObjectChoseCanvas.SetActive(false);
        gameObjectCanvasButton.SetActive(true);
    }
    public void ButtonLeft()
    {
        gameObjectButtonLeft.SetActive(false);
        gameObjectCanvasWarring.SetActive(true);
    }
    public void ButtonRight()
    {
        gameObjectButtonRight.SetActive(false);
        gameObjectButtonLeft.SetActive(true);
    }
    public void ButtonPlay()
    {
        gameObjectCanvasWarring.SetActive(false);
        gameObjectChoseCanvas.SetActive(true);
        Destroy(gameObject);
    }
}
