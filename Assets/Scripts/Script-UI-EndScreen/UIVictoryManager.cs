using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIVictoryManager : MonoBehaviour
{
    [Header("Victory Panel")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button exitVictoryButton;

    [Header("Defeat Panel")]
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button exitDefeatButton;

    private void Awake()
    {
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (defeatPanel != null) defeatPanel.SetActive(false);
    }

    private void Start()
    {
        if (nextLevelButton != null) nextLevelButton.onClick.AddListener(OnNextLevel);
        if (exitVictoryButton != null) exitVictoryButton.onClick.AddListener(OnExitToMenu);
        if (tryAgainButton != null) tryAgainButton.onClick.AddListener(OnRetryLevel);
        if (exitDefeatButton != null) exitDefeatButton.onClick.AddListener(OnExitToMenu);

        int resultado = PlayerPrefs.GetInt("GameResult", -1);
        int nivelActual = PlayerPrefs.GetInt("NivelJugador", 1);

        Debug.Log("[UIVictoryManager] Resultado leído de PlayerPrefs: " + resultado);
        Debug.Log("[UIVictoryManager] Nivel actual leído de PlayerPrefs: " + nivelActual);

        if (resultado == 1)
        {
            MostrarVictoria();

            // 🔒 Desactivar botón si es el último nivel
            if (nivelActual > 5 && nextLevelButton != null)
            {
                nextLevelButton.interactable = false;
                Debug.Log("[UIVictoryManager] Último nivel alcanzado. Botón siguiente nivel desactivado.");
            }
        }
        else if (resultado == 0)
        {
            MostrarDerrota();
        }
        else
        {
            Debug.LogWarning("[UIVictoryManager] Resultado inválido, mostrando panel de derrota.");
            MostrarDerrota();
        }
    }

    private void MostrarVictoria()
    {
        Debug.Log("[UIVictoryManager] Mostrando panel de victoria.");
        if (victoryPanel != null) victoryPanel.SetActive(true);
        if (defeatPanel != null) defeatPanel.SetActive(false);
    }

    private void MostrarDerrota()
    {
        Debug.Log("[UIVictoryManager] Mostrando panel de derrota.");
        if (defeatPanel != null) defeatPanel.SetActive(true);
        if (victoryPanel != null) victoryPanel.SetActive(false);
    }

    private void OnNextLevel()
    {
        Debug.Log("[UIVictoryManager] Botón siguiente nivel presionado.");
        SceneManager.LoadScene("MejorasScene");
    }

    private void OnRetryLevel()
    {
        Debug.Log("[UIVictoryManager] Botón reintentar presionado.");
        SceneManager.LoadScene("GAME");
    }

    private void OnExitToMenu()
    {
        Debug.Log("[UIVictoryManager] Botón salir al menú presionado.");
        SceneManager.LoadScene("StartMenu");
    }
}
