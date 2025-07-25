using UnityEngine;

public class CardsInfoButton : MonoBehaviour
{
    [SerializeField] private GameObject cardsInfoPanel;
    [SerializeField] private ExitCardsInfoManager exitCardsInfoManager;
    [SerializeField] private AudioClip bookSFX;

    [SerializeField] private CardsInfoOrigen origen;

    public void MostrarPanelInfo()
    {
        cardsInfoPanel.SetActive(true);
        exitCardsInfoManager.SetOrigen(origen); // Ya pausa y bloquea desde ahí
    }

    public void PlaySFX()
    {
        if (bookSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("book turn page");
        }
        else
        {
            Debug.LogWarning("Falta el sonido o AudioManager");
        }
    }
}
