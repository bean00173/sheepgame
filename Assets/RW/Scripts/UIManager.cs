using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text sheepSavedText;
    public Text sheepDroppedText;
    public GameObject gameOverWindow;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    public void UpdateSheepSaved()
    {
        sheepSavedText.text = GameManager.Instance.sheepSaved.ToString();
    }

    public void UpdateSheepDropped()
    {
        sheepDroppedText.text = GameManager.Instance.sheepDropped.ToString();
    }

    public void ShowGameOverWindow()
    {
        gameOverWindow.SetActive(true);
    }

}
