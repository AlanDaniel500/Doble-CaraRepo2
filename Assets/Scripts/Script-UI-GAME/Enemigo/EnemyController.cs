using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(Animator))]
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

    [Header("Efectos de Veneno")]
    [SerializeField] private int turnosConVeneno = 0;
    [SerializeField] private int dañoVenenoPorTurno = 0;

    [Header("Efectos de Oscuridad (reducción de daño)")]
    [SerializeField] private int turnosConReduccion = 0;
    [SerializeField] private int reduccionDaño = 0;

    private Animator animator;
    private PlayerHealthUI playerHealthUI;
    private bool estaActivo = false;

    public bool EstaActivo() => estaActivo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerHealthUI = FindFirstObjectByType<PlayerHealthUI>();
        Debug.Log("[EnemyController] Start: PlayerHealthUI encontrado.");

        vidaActual = vidaMaxima;
        turnosRestantes = turnosParaAtacar;
        ActualizarUICompleta();
        estaActivo = true;
    }

    public void SetStats(int nuevaVida, int nuevoDaño, int nuevosTurnos, Sprite nuevoSprite, RuntimeAnimatorController nuevoAnimador = null, string animacionIdle = "")
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

        if (animator != null)
        {
            if (nuevoAnimador != null)
            {
                animator.runtimeAnimatorController = nuevoAnimador;
                Debug.Log("[EnemyController] AnimatorController actualizado.");
            }

            if (!string.IsNullOrEmpty(animacionIdle))
            {
                animator.Play(animacionIdle);
                Debug.Log($"[EnemyController] Animación Idle '{animacionIdle}' reproducida.");
            }
        }

        ActualizarUICompleta();
        estaActivo = true;
        Debug.Log("[EnemyController] Enemigo activado con nuevos stats.");
    }

    public void OnPlayerTurnEnd()
    {
        if (!estaActivo) return;

        if (turnosConVeneno > 0)
        {
            Debug.Log($"[EnemyController] Veneno activo: {turnosConVeneno} turnos restantes. Aplicando {dañoVenenoPorTurno} daño.");
            AplicarDanoDesdeCombo(dañoVenenoPorTurno);
            turnosConVeneno--;

            if (turnosConVeneno <= 0)
            {
                dañoVenenoPorTurno = 0;
                Debug.Log("[EnemyController] Veneno ha terminado.");
            }
        }

        if (turnosConReduccion > 0)
        {
            turnosConReduccion--;

            if (turnosConReduccion <= 0)
            {
                reduccionDaño = 0;
                Debug.Log("[EnemyController] Reducción de daño (oscuridad) ha terminado.");
            }
        }

        turnosRestantes--;

        if (turnosRestantes <= 0)
        {
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

        int dañoFinal = daño;

        if (turnosConReduccion > 0)
        {
            dañoFinal -= reduccionDaño;
            if (dañoFinal < 0) dañoFinal = 0;
            Debug.Log($"[EnemyController] Ataque reducido por oscuridad: {dañoFinal} (original: {daño})");
        }

        Debug.Log("[EnemyController] ¡El enemigo ataca con daño final: " + dañoFinal + "!");

        if (playerHealthUI != null)
        {
            playerHealthUI.TakeDamage(dañoFinal);
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

            if (LevelManager.Instance != null)
            {
                Debug.Log("[EnemyController] Solicitando subir de nivel a LevelManager...");
                LevelManager.Instance.SubirDeNivel();
            }
            else
            {
                Debug.LogWarning("[EnemyController] LevelManager.Instance es null al intentar subir de nivel.");
            }
        }
    }

    public void AplicarVeneno(int dañoPorTurno, int turnos)
    {
        if (!estaActivo) return;

        dañoVenenoPorTurno = dañoPorTurno;
        turnosConVeneno = turnos;

        Debug.Log($"[EnemyController] Aplicado veneno: {dañoPorTurno} daño por turno durante {turnos} turnos.");
    }

    public void AplicarDebuffDaño(int cantidad, int turnos)
    {
        if (!estaActivo) return;

        reduccionDaño = cantidad;
        turnosConReduccion = turnos;

        Debug.Log($"[EnemyController] Aplicada reducción de daño: -{cantidad} por {turnos} turnos.");
    }

    public void RetrasarProximoAtaque()
    {
        if (!estaActivo) return;

        turnosRestantes++;
        ActualizarTextoTurnos();
        Debug.Log("[EnemyController] Turno de ataque retrasado +1");
    }

    private void ActualizarTextoVida() => textoVida.text = vidaActual.ToString();
    private void ActualizarTextoDaño() => textoDaño.text = daño.ToString();
    private void ActualizarTextoTurnos() => textoTurnos.text = turnosRestantes.ToString();
    private void ActualizarUICompleta()
    {
        ActualizarTextoVida();
        ActualizarTextoDaño();
        ActualizarTextoTurnos();
    }

    public int VidaActual => vidaActual;
}
