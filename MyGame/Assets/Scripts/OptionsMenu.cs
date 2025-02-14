using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public Slider brightnessSlider;
    public Slider soundSlider;
    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        soundSlider.onValueChanged.AddListener(SetSound);
    }
    

    void SetBrightness(float value)
    {
        // Adjust the brightness based on the slider value
        // Need to finalise this to ensure ti works
        RenderSettings.ambientLight = Color.white * value;
    }

    void SetSound(float value)
    {
        // Adjust the sound volume based on the slider value
        // Need to finalise this to ensure ti works

        AudioListener.volume = value;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void CloseOptionsMenu()
    {
        SceneManager.UnloadSceneAsync("OptionsMenu");
        Time.timeScale = 1f;
    }
}
