using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    [SerializeField] private CanvasGroup gameCanvasGroup; // El canvas con las cartas y todo el juego

    [Header("Paneles")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject audioPanel;
    [SerializeField] private GameObject cardsInfoPanel;

    void Start()
    {
        // Aseguramos que el menú de pausa esté cerrado al iniciar
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    // Botón Pause - abre menú pausa
    public void OnOpenPauseMenu()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        // Opcional: pausá el juego aquí si querés

        gameCanvasGroup.interactable = false;  // No deja interactuar con el juego
        gameCanvasGroup.blocksRaycasts = false;
        gameCanvasGroup.alpha = 1f;

        Time.timeScale = 0f;
    }

    // Botón Exit Game
    public void OnExitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }

    // Botón Audio - abre panel Audio
    public void OnAudioPressed()
    {
        if (audioPanel != null)
        {
            audioPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }

    // Botón Cards Info - abre panel Cards Info
    public void OnCardsInfoPressed()
    {
        if (cardsInfoPanel != null)
        {
            cardsInfoPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
    }

    // Botón X de panel Audio - vuelve a Pause Panel
    public void OnCloseAudioPanel()
    {
        if (audioPanel != null)
            audioPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    // Botón X de panel Cards Info - vuelve a Pause Panel
    public void OnCloseCardsInfoPanel()
    {
        if (cardsInfoPanel != null)
            cardsInfoPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    // Botón X del menú de pausa - cierra el menú de pausa
    public void OnClosePauseMenu()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Opcional: reanudar el juego si pausaste

        gameCanvasGroup.interactable = true;  // Vuelve a permitir interacción
        gameCanvasGroup.blocksRaycasts = true;
        gameCanvasGroup.alpha = 1f;

        Time.timeScale = 1f;
    }
}
