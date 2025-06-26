using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    private AudioSource musicSource;

    private void Start()
    {
        musicSource = AudioManager.Instance.GetComponentInChildren<AudioSource>();

        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (sceneName == "StartMenu" && musicSource.clip != menuMusic)
        {
            musicSource.clip = menuMusic;
            musicSource.Play();
        }
        else if (sceneName == "GAME" && musicSource.clip != gameMusic)
        {
            musicSource.clip = gameMusic;
            musicSource.Play();
        }
    }
}

