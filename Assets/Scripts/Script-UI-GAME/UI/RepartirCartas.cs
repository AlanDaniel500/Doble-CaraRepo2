using UnityEngine;

public class RepartirCartas : MonoBehaviour
{
    [SerializeField] private CardSpawner cardSpawner;

    public void RepartirUnaCarta()
    {
        if (cardSpawner == null)
        {
            Debug.LogWarning("CardSpawner no asignado en RepartirCartas.");
            return;
        }

        if (cardSpawner.CantidadCartasEnJuego() >= 8)
        {
            Debug.Log("Ya tenés el máximo de 8 cartas.");
            return;
        }

        cardSpawner.RepartirCartaIndividual();
    }
}
