using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    public Button startButton;
    // Start is called before the first frame update
    void Start()
    {
        if (startButton == null)
        {
            Debug.LogError("Start Button is not assigned.");
            return;
        }
        startButton.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
