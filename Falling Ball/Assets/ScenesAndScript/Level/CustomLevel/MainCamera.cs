using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    LevelSetting levelSetting;
    public List<Vector3> Vector3sCameraPosition = new List<Vector3>(0);
    public List<float> FloatCameraSpeed = new List<float>(0);
    public List<Color> ColorsBackground = new List<Color>(0);
    public bool MovingCamera = false;


}
