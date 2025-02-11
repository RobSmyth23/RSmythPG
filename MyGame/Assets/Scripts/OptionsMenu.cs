using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public Slider brightnessSlider;
    public Slider soundSlider;
    // Start is called before the first frame update
    void Start()
    {
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        soundSlider.onValueChanged.AddListener(SetSound);
    }
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    void SetBrightness(float value)
    {
        // Adjust the brightness based on the slider value
        RenderSettings.ambientLight = Color.white * value;
    }

    void SetSound(float value)
    {
        // Adjust the sound volume based on the slider value
        AudioListener.volume = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
