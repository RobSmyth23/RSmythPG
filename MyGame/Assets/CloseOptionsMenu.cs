using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseOptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void CloseOptionsMenuScene()
    {
        SceneManager.UnloadSceneAsync("OptionsMenu");
        Time.timeScale = 1f;
    }
}
