using UnityEngine;

public class CardSelector : MonoBehaviour
{
    private bool isSelected = false;
    private Vector3 originalPosition;
    public float liftAmount = 0.3f;

    // Contador estático para todas las cartas
    private static int cartasLevantadas = 0;
    private static int maxCartasLevantadas = 4;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            // Solo levantar si no se superó el máximo
            if (cartasLevantadas < maxCartasLevantadas)
            {
                transform.position = originalPosition + new Vector3(0, liftAmount, 0);
                isSelected = true;
                cartasLevantadas++;
            }
            else
            {
                Debug.Log("Ya hay 4 cartas levantadas. Baja alguna antes de levantar otra.");
            }
        }
        else
        {
            // Bajar carta y descontar del contador
            transform.position = originalPosition;
            isSelected = false;
            cartasLevantadas--;
        }
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    // Método estático para reiniciar el contador
    public static void ReiniciarContador()
    {
        cartasLevantadas = 0;
    }
}
