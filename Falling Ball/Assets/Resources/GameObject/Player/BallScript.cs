using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BallScript : MonoBehaviour
{
    public int Force =500;
    public MainMenuCustomEditor mainMenuCustomEditor;
    public LevelSetting levelSetting;

    private new Rigidbody2D rigidbody2D;
    private bool moveLeft = false;
    private bool moveRight = false;
    private void Start()
    {
        moveLeft = false;
        moveRight = false;
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (moveLeft && !moveRight)
        {
            rigidbody2D.AddForce(Vector2.left * Force* Time.deltaTime, 0);
        }
        else if (moveRight && !moveLeft)
        {
            rigidbody2D.AddForce(Vector2.right * Force * Time.deltaTime, 0);
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

        if (collision.gameObject.name.Contains("Island"))
        {
            int gold = int.Parse(levelSetting.gameObjectCoinText.GetComponent<Text>().text);
            gold += collision.gameObject.GetComponent<IslandScript>().ValueMoney;
            levelSetting.gameObjectCoinText.GetComponent<Text>().text = gold.ToString();
            PlayerPrefs.SetInt(GlobalVariable.GoldPlayerPrefs, gold);
            collision.gameObject.name = "Check";
        }
        else if (collision.gameObject.name.Contains("Mine") == true || collision.gameObject.name == "UpLimit" ||
            collision.gameObject.name == "DownLimit")
        {
#if GOD_MODE
#else
            mainMenuCustomEditor.ShowGameOver();
            collision.gameObject.name = "Check";
#endif
        }
        else if (collision.gameObject.name.Contains("Start"))
        {
            levelSetting.gameObjectMainCamera.GetComponent<MainCamera>().MovingCamera = false;
        }
        else if (collision.gameObject.name.Contains("End"))
        {
            Time.timeScale = 0;
            mainMenuCustomEditor.ShowLevelComplete();
            collision.gameObject.name = "Check";
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Start"))
        {
            levelSetting.GlobalMoveIsland = true;
            levelSetting.GlobalMoveMine = true;
            levelSetting.gameObjectMainCamera.GetComponent<MainCamera>().MovingCamera = true;
            collision.gameObject.name = "Check";
        }
    }
}
