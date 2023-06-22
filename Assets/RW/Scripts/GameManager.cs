using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [HideInInspector]
    public int sheepSaved;

    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver;

    public float timeBeforeSpeedIncrease = 5; // A variable to store time between speed increments
    public float speedMultiplier = 1.5f;
    private float timer;
    private float increaseIndex;
    public float sheepRunSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            SceneManager.LoadScene("Title");
        }

        if (timer < timeBeforeSpeedIncrease) // check if timer less than necessary for increment
        {
            timer += Time.deltaTime; // if true increase time
        }
        else if (timer >= timeBeforeSpeedIncrease) // else check if timer is ready to increment
        {
            timer = 0; // reset timer for next increment
            increaseIndex++;
            if (increaseIndex < 5) // if speed hasn't increased more than max of 5 times
            {
                sheepRunSpeed = sheepRunSpeed + speedMultiplier; // new speed
            }
        }
    }

    public void SheepSaved()
    {
        sheepSaved++;
        UIManager.Instance.UpdateSheepSaved();
    }

    public void SheepDropped()
    {
        sheepDropped++;
        UIManager.Instance.UpdateSheepDropped();

        if(sheepDropped == sheepDroppedBeforeGameOver)
        {
            GameOver();   
        }
    }

    public void GameOver()
    {
        if(sheepSaved > GameSettings.highScore) // Add Method to GameOver Script to check if the current game Score is greater than the stored High Score
        {
            GameSettings.highScore = sheepSaved; // update high score
        }

        SheepSpawner.Instance.canSpawn = false;
        SheepSpawner.Instance.DestroyAllSheep();

        StartCoroutine(GameOverScreen());
    }

    IEnumerator GameOverScreen()
    {
        yield return new WaitForSeconds(1);
        UIManager.Instance.ShowGameOverWindow();
    }
}
