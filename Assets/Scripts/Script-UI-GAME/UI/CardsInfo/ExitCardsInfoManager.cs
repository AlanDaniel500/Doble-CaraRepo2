using UnityEngine;

public class ExitCardsInfoManager : MonoBehaviour
{
    private CardsInfoOrigen origen;

    [SerializeField] private GameObject cardsInfoPanel;
    [SerializeField] private GameObject menuPausaPanel;
    [SerializeField] private AudioClip bookSFX;

    [SerializeField] private CanvasGroup gameCanvasGroup;

    public void SetOrigen(CardsInfoOrigen nuevoOrigen)
    {
        origen = nuevoOrigen;

        // Al abrir el panel, pausamos el juego y bloqueamos interacción
        if (gameCanvasGroup != null)
        {
            gameCanvasGroup.interactable = false;
            gameCanvasGroup.blocksRaycasts = false;
            gameCanvasGroup.alpha = 1f;
        }

        Time.timeScale = 0f;
    }

    public void OnExitButtonPressed()
    {
        cardsInfoPanel.SetActive(false);

        if (origen == CardsInfoOrigen.MenuPausa)
        {
            if (menuPausaPanel != null)
                menuPausaPanel.SetActive(true);
            // No reanudamos el juego, sigue en pausa
        }
        else if (origen == CardsInfoOrigen.MenuPrincipal)
        {
            // Reactivar el canvas del juego
            if (gameCanvasGroup != null)
            {
                gameCanvasGroup.interactable = true;
                gameCanvasGroup.blocksRaycasts = true;
                gameCanvasGroup.alpha = 1f;
            }

            Time.timeScale = 1f; // Reanudar el juego
        }
    }

    public void PlaySFX()
    {
        if (bookSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("book closing");
        }
        else
        {
            Debug.LogWarning("Falta el sonido o AudioManager");
        }
    }

}
