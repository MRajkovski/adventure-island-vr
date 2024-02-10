using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
    }

    public void ToggleCheats()
    {
        if (PlayerPrefs.GetInt("Cheats", 0) == 0)
        {
            PlayerPrefs.SetInt("Cheats", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Cheats", 1);
        }
    }
    public void ToggleFog()
    {
        if(RenderSettings.fog == true)
        {
            RenderSettings.fog = false;
        }
        else
        {
            RenderSettings.fog = true;
        }
        
    }
    public void ChangeVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        AudioListener.volume = volumeSlider.value;
    }

}
