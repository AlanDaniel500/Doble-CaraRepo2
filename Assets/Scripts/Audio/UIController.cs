using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider Master_Slider,Music_Slider, SFX_Slider;

    private void Start()
    {
        if (Master_Slider != null)
        {
            float masterVol = PlayerPrefs.GetFloat("MasterVolume", 1f);
            Master_Slider.value = masterVol;
            AudioManager.Instance.SetMasterVolume(masterVol);
        }

        // Ya tenías esto:
        if (Music_Slider != null)
        {
            float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
            Music_Slider.value = musicVol;
            AudioManager.Instance.SetMusicVolume(musicVol);
        }
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        //AudioManager.Instance.ToggleSFX();
    }

    public void MasterVolume()
    {
        if (Master_Slider != null)
            AudioManager.Instance.SetMasterVolume(Master_Slider.value);
    }

    public void MusicVolume()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetMusicVolume(Music_Slider.value);
        }
    }

    /*public void SFXVolume()
    {
        if (SFX_Slider != null)
            AudioManager.Instance.SetSFXVolume(SFX_Slider.value);
    }*/
}

