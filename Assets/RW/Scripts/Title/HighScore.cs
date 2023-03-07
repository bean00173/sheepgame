using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text hscoreText = this.GetComponent<Text>();

        hscoreText.text = GameSettings.highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
