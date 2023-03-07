using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    Scene scene;
    Text hscoreText;
    // Start is called before the first frame update
    void Start()
    {
        hscoreText = this.GetComponent<Text>();

        hscoreText.text = GameSettings.highScore.ToString();

        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(scene.name == "Game" && GameSettings.highScore < GameManager.Instance.sheepSaved)
        {
            hscoreText.text = GameManager.Instance.sheepSaved.ToString();
        }
    }
}
