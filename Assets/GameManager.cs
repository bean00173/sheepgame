using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public int sheepSaved;

    [HideInInspector]
    public int sheepDropped;

    public int sheepDroppedBeforeGameOver;

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
