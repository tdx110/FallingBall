using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScallingObject : MonoBehaviour
{
    public RectTransform DefautCanvasRecttransform;

    void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = DefautCanvasRecttransform.sizeDelta;
        gameObject.GetComponent<RectTransform>().localScale = DefautCanvasRecttransform.localScale;
    }


}
