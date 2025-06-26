using UnityEngine;
using TMPro;

public class RecibeDanoEnemigo : MonoBehaviour
{
    [SerializeField] private int vidaMaxima = 100;
    private int vidaActual;

    [SerializeField] private TextMeshProUGUI textoVida;

    public int VidaActual => vidaActual;  // Propiedad pública para leer vida actual

    private void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarTextoVida();
    }

    public void AplicarDanoDesdeCombo(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;

        Debug.Log($"Enemigo recibió {cantidad} de daño. Vida restante: {vidaActual}");
        ActualizarTextoVida();

        if (vidaActual == 0)
        {
            Debug.Log("¡El enemigo ha sido derrotado!");
            // Lógica para muerte del enemigo si hace falta
        }
    }

    private void ActualizarTextoVida()
    {
        if (textoVida != null)
        {
            textoVida.text = vidaActual.ToString();
        }
    }
}
