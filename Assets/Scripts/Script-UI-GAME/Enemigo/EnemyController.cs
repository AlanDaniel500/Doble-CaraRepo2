using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] private int daño = 100;
    [SerializeField] private int turnosParaAtacar = 3;
    private int vidaActual;
    private int turnosRestantes;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoVida;
    [SerializeField] private TextMeshProUGUI textoDaño;
    [SerializeField] private TextMeshProUGUI textoTurnos;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Delays para mostrar el 0 y ataque")]
    [SerializeField] private float delayAntesDeAtacar = 1f;
    [SerializeField] private float delayDespuesDeAtacar = 1f;

    private PlayerHealthUI playerHealthUI;

    private void Start()
    {
        playerHealthUI = FindFirstObjectByType<PlayerHealthUI>();

        // IMPORTANTE: No inicializamos stats aquí. Esperamos que LevelManager los aplique con SetStats().
        // Solo actualizamos UI si los valores ya fueron seteados antes de que Start() se llame.

        ActualizarTextoVida();
        ActualizarTextoDaño();
        ActualizarTextoTurnos();
    }

    public void SetStats(int nuevaVida, int nuevoDaño, int nuevosTurnos, Sprite nuevoSprite)
    {
        vidaMaxima = nuevaVida;
        vidaActual = nuevaVida;
        daño = nuevoDaño;
        turnosParaAtacar = nuevosTurnos;
        turnosRestantes = nuevosTurnos;

        if (spriteRenderer != null && nuevoSprite != null)
            spriteRenderer.sprite = nuevoSprite;

        ActualizarTextoVida();
        ActualizarTextoDaño();
        ActualizarTextoTurnos();
    }

    public void OnPlayerTurnEnd()
    {
        turnosRestantes--;

        if (turnosRestantes <= 0)
        {
            if (textoTurnos != null)
                textoTurnos.text = "0";

            StartCoroutine(TriggerEnemyActionConDelays());
        }
        else
        {
            ActualizarTextoTurnos();
        }
    }

    private IEnumerator TriggerEnemyActionConDelays()
    {
        yield return new WaitForSeconds(delayAntesDeAtacar);

        EjecutarAtaque();

        yield return new WaitForSeconds(delayDespuesDeAtacar);

        ResetTurnos();
    }

    private void ResetTurnos()
    {
        turnosRestantes = turnosParaAtacar;
        ActualizarTextoTurnos();
    }

    public void EjecutarAtaque()
    {
        Debug.Log("¡El enemigo ataca!");

        if (playerHealthUI != null)
        {
            playerHealthUI.TakeDamage(daño);
        }

        if (CameraShake.Instance != null)
        {
            StartCoroutine(CameraShake.Instance.Shake(0.2f, 0.15f));
        }

        ActualizarTextoDaño();
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
            // Aquí podés agregar lógica extra al morir
        }
    }

    private void ActualizarTextoVida()
    {
        if (textoVida != null)
            textoVida.text = vidaActual.ToString();
    }

    private void ActualizarTextoDaño()
    {
        if (textoDaño != null)
            textoDaño.text = daño.ToString();
    }

    private void ActualizarTextoTurnos()
    {
        if (textoTurnos != null)
            textoTurnos.text = turnosRestantes.ToString();
    }

    public int VidaActual => vidaActual;
}
