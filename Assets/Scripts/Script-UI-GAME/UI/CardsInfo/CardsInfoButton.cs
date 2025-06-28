using UnityEngine;

public class CardsInfoButton : MonoBehaviour
{
    [SerializeField] private GameObject cardsInfoPanel;
    [SerializeField] private ExitCardsInfoManager exitCardsInfoManager;

    [SerializeField] private CardsInfoOrigen origen;

    public void MostrarPanelInfo()
    {
        cardsInfoPanel.SetActive(true);
        exitCardsInfoManager.SetOrigen(origen); // Ya pausa y bloquea desde ahí
    }
}
