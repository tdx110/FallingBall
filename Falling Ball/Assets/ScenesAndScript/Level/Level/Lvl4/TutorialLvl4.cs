using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLvl4 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) Time.timeScale = 0;
    }

    public void Disable()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
