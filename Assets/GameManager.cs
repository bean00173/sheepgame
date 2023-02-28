using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [HideInInspector]
    public int sheepSaved;

    [HideInInspector]
    public int sheepDropped;

    [HideInInspector]
    public int highScore;

    public int sheepDroppedBeforeGameOver;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            ResetGame();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            SceneManager.LoadScene("Title");
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
        if(sheepSaved > highScore)
        {
            highScore = sheepSaved;
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

    public void ResetGame()
    {
        sheepSaved = 0;
        sheepDropped = 0;
        SheepSpawner.Instance.canSpawn = true;
    }
}
