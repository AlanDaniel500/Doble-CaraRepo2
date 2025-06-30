using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject cardsInfoPanel;

    [SerializeField] private ExitCardsInfoManager exitCardsInfoManager; // Referencia al script nuevo

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void OnOpenPauseMenu()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void OnExitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void OnAudioPressed()
    {
        if (audioPanel != null)
        {
            audioPanel.SetActive(true);
            if (pausePanel != null)
                pausePanel.SetActive(false);
        }
    }

    public void OnCardsInfoPressed()
    {
        if (cardsInfoPanel != null)
        {
            cardsInfoPanel.SetActive(true);
            if (pausePanel != null)
                pausePanel.SetActive(false);
        }

        if (exitCardsInfoManager != null)
        {
            exitCardsInfoManager.SetOrigen(CardsInfoOrigen.MenuPausa);
        }
    }

    public void OnCloseAudioPanel()
    {
        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void OnCloseCardsInfoPanel()
    {
        if (cardsInfoPanel != null)
            cardsInfoPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void OnClosePauseMenu()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
}
