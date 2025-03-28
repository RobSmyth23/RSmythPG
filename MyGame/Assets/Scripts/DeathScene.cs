using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    public float delayBeforeMainMenu = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadMainMenuAfterDelay(delayBeforeMainMenu));
    }
    public void LoadDeathScene()
    {
        
        // Load the DeathScreen scene
       // SceneManager.LoadScene("DeathScreen");

        // Start a coroutine to load the Main Menu after a delay
        
    }
    private System.Collections.IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        // Wait for the delay
        yield return new WaitForSeconds(delay);

        // Load the Main Menu scene
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
