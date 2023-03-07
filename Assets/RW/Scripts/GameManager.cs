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

    public float timeBeforeSpeedIncrease = 5;
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

        if (timer < timeBeforeSpeedIncrease)
        {
            timer += Time.deltaTime;
        }
        else if (timer >= timeBeforeSpeedIncrease)
        {
            timer = 0;
            increaseIndex++;
            if (increaseIndex < 5)
            {
                sheepRunSpeed = sheepRunSpeed + speedMultiplier;
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
        if(sheepSaved > GameSettings.highScore)
        {
            GameSettings.highScore = sheepSaved;
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
