using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{

    private Text hs;
    // Start is called before the first frame update
    void Start()
    {
        hs = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hs.text = GameManager.Instance.highScore.ToString();
    }
}
