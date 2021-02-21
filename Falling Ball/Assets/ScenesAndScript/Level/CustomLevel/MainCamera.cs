using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Image imageBackgroiund;

    [HideInInspector]
    LevelSetting levelSetting;
    [HideInInspector]
    public List<Vector3> Position = new List<Vector3>(0);
    [HideInInspector]
    public List<float> Speed = new List<float>(0);
    [HideInInspector]
    public List<Color> ColorsBackground = new List<Color>(0);

    public bool MovingCamera = false;

    private Vector3 actualPosition;
    private int step;
    private Color deltaColor;

    private void Start()
    {
        actualPosition = this.gameObject.transform.position;
        deltaColor = imageBackgroiund.color;
        step = 0;
    }
    private void Update()
    {
        if (MovingCamera)
        {
            moveCamera();
        }
    }

    //Funkcja odpowiadaj¹ca za poruszanie obiektem
    private void moveCamera()
    {
        if (actualPosition == Position[step])
        {//Przechodzi do nastêpnego kroku
            if (step != (Position.Count - 1)) step++;
        }
        else
        {
            float deltaDistance;
            float distance;

            if (step == 0) deltaDistance = Time.deltaTime * Speed[step];
            else deltaDistance = Time.deltaTime * Speed[step - 1];

            distance = Vector3.Magnitude(gameObject.transform.position - Position[step]);
            deltaColor = (imageBackgroiund.color - ColorsBackground[step]) / distance;
            if (Mathf.Abs(actualPosition.x - Position[step].x) > deltaDistance)
            {
                if (actualPosition.x < Position[step].x) actualPosition += new Vector3(deltaDistance, 0, 0);
                else actualPosition -= new Vector3(deltaDistance, 0, 0);
            }
            else actualPosition = new Vector3(Position[step].x, actualPosition.y, actualPosition.z);

            if (Mathf.Abs(actualPosition.y - Position[step].y) > deltaDistance)
            {
                if (actualPosition.y < Position[step].y) actualPosition += new Vector3(0, deltaDistance, 0);
                else actualPosition -= new Vector3(0, deltaDistance, 0);
            }
            else actualPosition = new Vector3(actualPosition.x, Position[step].y, actualPosition.z);
            gameObject.transform.position = actualPosition;
            imageBackgroiund.color -= deltaColor * deltaDistance;
        }
    }
}
