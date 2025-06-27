using UnityEngine;

public class CardSelector : MonoBehaviour
{
    private bool isSelected = false;
    private Vector3 originalPosition;
    public float liftAmount = 0.3f;

    // Contador estático para todas las cartas
    private static int cartasLevantadas = 0;

    // Límite editable desde el Inspector (por ejemplo 8)
    [SerializeField] private int maxCartasPermitidas = 8;

    // Necesitamos guardar una copia del límite para accederlo de forma estática
    private static int maxCartasLevantadas;

    private void Start()
    {
        originalPosition = transform.position;

        // Inicializamos el valor estático en el primer Start
        if (maxCartasLevantadas == 0)
        {
            maxCartasLevantadas = maxCartasPermitidas;
        }
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
                Debug.Log($"Ya hay {maxCartasLevantadas} cartas levantadas. Baja alguna antes de levantar otra.");
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
