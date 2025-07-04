using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] private int daño = 100;
    [SerializeField] private int turnosParaAtacar = 3;
    private int vidaActual = 0;
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
    private bool estaActivo = false;

    public bool EstaActivo()
    {
        return estaActivo;
    }

    private void Start()
    {
        playerHealthUI = FindFirstObjectByType<PlayerHealthUI>();
        Debug.Log("[EnemyController] Start: PlayerHealthUI encontrado.");

        // No actualizamos stats ni UI aquí, lo hará LevelManager con SetStats
    }

    public void SetStats(int nuevaVida, int nuevoDaño, int nuevosTurnos, Sprite nuevoSprite)
    {
        Debug.Log($"[EnemyController] SetStats llamado: vida={nuevaVida}, daño={nuevoDaño}, turnos={nuevosTurnos}");

        vidaMaxima = nuevaVida;
        vidaActual = nuevaVida;
        daño = nuevoDaño;
        turnosParaAtacar = nuevosTurnos;
        turnosRestantes = nuevosTurnos;

        if (spriteRenderer != null && nuevoSprite != null)
        {
            spriteRenderer.sprite = nuevoSprite;
            Debug.Log("[EnemyController] Sprite actualizado.");
        }

        ActualizarTextoVida();
        ActualizarTextoDaño();
        ActualizarTextoTurnos();

        estaActivo = true;
        Debug.Log("[EnemyController] Enemigo activado con nuevos stats.");
    }

    public void OnPlayerTurnEnd()
    {
        if (!estaActivo) return;

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
        if (!estaActivo) return;

        Debug.Log("[EnemyController] ¡El enemigo ataca!");

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
        if (!estaActivo) return;

        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;

        Debug.Log($"[EnemyController] Daño recibido: {cantidad}. Vida restante: {vidaActual}");
        ActualizarTextoVida();

        if (vidaActual == 0)
        {
            Debug.Log("[EnemyController] ¡El enemigo ha sido derrotado!");

            estaActivo = false;

            // Aquí llamamos a subir de nivel en LevelManager
            if (LevelManager.Instance != null)
            {
                Debug.Log("[EnemyController] Solicitando subir de nivel a LevelManager...");
                LevelManager.Instance.SubirDeNivel();
            }
            else
            {
                Debug.LogWarning("[EnemyController] LevelManager.Instance es null al intentar subir de nivel.");
            }

            // Podés agregar animación de muerte o lógica de muerte aquí
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
