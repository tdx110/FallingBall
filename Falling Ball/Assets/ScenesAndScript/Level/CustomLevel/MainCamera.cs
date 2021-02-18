using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [HideInInspector]
    LevelSetting levelSetting;
    [HideInInspector]
    public List<Vector3> Vector3sCameraPosition = new List<Vector3>(0);
    [HideInInspector]
    public List<float> FloatCameraSpeed = new List<float>(0);
    [HideInInspector]
    public List<Color> ColorsBackground = new List<Color>(0);
    [HideInInspector]
    public bool MovingCamera = false;


}
