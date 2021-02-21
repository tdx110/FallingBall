using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLvl3 : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale != 0) Time.timeScale = 0;   
    }

    public void Destroy()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
