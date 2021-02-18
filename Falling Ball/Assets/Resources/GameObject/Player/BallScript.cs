using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    private MainMenuCustomEditor mainMenuCustomEditor;
    [HideInInspector]
    public Vector2 Force = new Vector2(100, 0);

    private new Rigidbody2D rigidbody2D;
    private bool moveLeft = false;
    private bool moveRight = false;
    private void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (moveLeft && !moveRight)
        {
            rigidbody2D.AddForce(Force*Time.deltaTime, ForceMode2D.Force);
        }
        else if (moveRight && !moveLeft)
        {
            rigidbody2D.AddForce(Force * Time.deltaTime, ForceMode2D.Force);
        }
    }
    public void MoveLeft()
    {
        if (moveLeft) moveLeft = false;
        else moveLeft = true;
    }
    public void MoveRight()
    {
        if (moveRight) moveRight = false;
        else moveRight = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
