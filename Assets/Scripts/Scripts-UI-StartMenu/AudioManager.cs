using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (Instance != this) return;

        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSource.volume = musicVol;
        //sfxSource.volume = sfxVol;

        // Si no hay música aún, arrancar la correcta
        if (musicSource.clip == null)
        {
            ChangeMusicByScene();
        }
    }

    private void ChangeMusicByScene()
    {
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "StartMenu":
                PlayMusic("MainTheme");
                break;

            case "GAME":
                PlayMusic("LevelTheme");
                break;

            case "MejorasScene":
                PlayMusic("UpgradeTheme");
                break;

            default:
                PlayMusic("MainTheme");
                break;
        }
    }



    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, s => s.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound Not Found: " + name);
            return;
        }

        musicSource.clip = s.clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    /*public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning("SFX Not Found: " + name);
            return;
        }

        sfxSource.PlayOneShot(s.clip);
    }*/

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    /*public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }*/

    public void SetMasterVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    /*public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }*/

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StartMenu")
        {
            PlayMusic("MainTheme");
        }
        else if (scene.name == "GAME")
        {
            PlayMusic("BGM-LEVELS");
        }
        // Agregar más escenas
    }


}
