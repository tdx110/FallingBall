using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Jobs;
//using Unity.Mathematics;
//using Unity.Jobs;
//using Unity.Collections;
//using Unity.Burst;

public class MineScript : MonoBehaviour
{
    [HideInInspector]
    public LevelSetting levelSetting;
    [HideInInspector]
    public List<Vector3> Position = new List<Vector3>(0);
    [HideInInspector]
    public List<float> Speed = new List<float>(0);
    [HideInInspector]
    public bool Moving = false;
    [HideInInspector]
    public bool MovingAlways = false;
    [HideInInspector]
    public bool DestroyWhenInvisible = false;


    private Vector3 actualPosition;
    private int step;

    private void Start()
    {
        actualPosition = this.gameObject.transform.position;
        step = 0;
    }
    private void Update()
    {
        if (levelSetting.GlobalMoveMine)
        {
            if (MovingAlways)
            {
                //Ciagle rusza si� mina
                if (Moving)
                {
                    moveMine();
                }
            }
            else
            {
                //Mina porusza sie w zasi�gu kamery
                if (gameobjectVisiable())
                {
                    if (Moving)
                    {
                        moveMine();
                    }
                }
            }
        }

        //Niszczy obiekt kiedy niewidoczny
        if (gameobjectVisiable()) DestroyWhenInvisible = true;
        else if (DestroyWhenInvisible && !gameobjectVisiable())
        {
            Destroy(gameObject);
        }
    }
    private bool gameobjectVisiable()
    {
        if (Mathf.Abs(gameObject.transform.position.y - levelSetting.gameObjectMainCamera.transform.position.y) < 6) return true;
        else return false;
    }

    //Funkcja odpowiadaj�ca za poruszanie obiektem
    private void moveMine()
    {
        if (actualPosition == Position[step])
        {//Przechodzi do nast�pnego kroku
            if (step == (Position.Count - 1)) step = 0;
            else step++;
        }
        else
        {
            float deltaDistance;
            

            if (step == (Position.Count))
            {//Je�li jest ostatni krok
                deltaDistance = Time.deltaTime * Speed[0];
                if (Mathf.Abs(actualPosition.x - Position[0].x) > deltaDistance)
                {
                    if (actualPosition.x < Position[0].x) actualPosition += new Vector3(deltaDistance, 0, 0) ;
                    else actualPosition -= new Vector3(deltaDistance, 0, 0);
                }
                else actualPosition = new Vector3(Position[0].x, actualPosition.y, actualPosition.z);
                
                if (Mathf.Abs(actualPosition.y - Position[0].y) > deltaDistance)
                {
                    if (actualPosition.y < Position[0].y) actualPosition += new Vector3(0, deltaDistance, 0);
                    else actualPosition -= new Vector3(0, deltaDistance, 0);
                }
                else actualPosition = new Vector3(actualPosition.x,Position[0].y, actualPosition.z);
            }
            else
            {
                deltaDistance = Time.deltaTime * Speed[step];
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
            }
            gameObject.transform.position = actualPosition;
        }
    }
}