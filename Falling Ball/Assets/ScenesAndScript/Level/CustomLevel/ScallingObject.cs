using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScallingObject : MonoBehaviour
{
    public RectTransform DefautCanvasRecttransform;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = DefautCanvasRecttransform.sizeDelta;
        gameObject.GetComponent<RectTransform>().localScale = DefautCanvasRecttransform.localScale;

    }
}
