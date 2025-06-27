using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerHealthUI playerHealthUI;

    public void Curar(int cantidad)
    {
        if (playerHealthUI != null)
        {
            playerHealthUI.Heal(cantidad);
        }
        else
        {
            Debug.LogWarning("PlayerHealthUI no asignado en Player");
        }
    }

    public void RecibirDaño(int cantidad)
    {
        if (playerHealthUI != null)
        {
            playerHealthUI.TakeDamage(cantidad);
        }
    }
}
