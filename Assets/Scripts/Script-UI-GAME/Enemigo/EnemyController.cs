using UnityEngine;
using UnityEngine.UI;
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

    [Header("Delays para mostrar el 0 y ataque")]
    [SerializeField] private float delayAntesDeAtacar = 1f;
    [SerializeField] private float delayDespuesDeAtacar = 1f;

    [Header("Efectos de Veneno")]
    [SerializeField] private int turnosConVeneno = 0;
    [SerializeField] private int dañoVenenoPorTurno = 0;

    [Header("Efectos de Oscuridad (reducción de daño)")]
    [SerializeField] private int turnosConReduccion = 0;
    [SerializeField] private int reduccionDaño = 0;

    [Header("Referencias UI")]
    [SerializeField] private Image enemyImageUI;

    private Animator animator;
    private PlayerHealthUI playerHealthUI;
    private bool estaActivo = false;

    public bool EstaActivo() => estaActivo;

    private void Awake()
    {
        if (enemyImageUI == null)
        {
            Debug.LogWarning("[EnemyController] Image UI no asignada.");
        }
        else
        {
            animator = enemyImageUI.GetComponent<Animator>();
            if (animator == null)
                Debug.LogWarning("[EnemyController] Animator no encontrado en Image UI.");
        }
    }

    private void Start()
    {
        playerHealthUI = FindFirstObjectByType<PlayerHealthUI>();
        vidaActual = vidaMaxima;
        turnosRestantes = turnosParaAtacar;
        ActualizarUICompleta();
        estaActivo = true;
    }

    /// <summary>
    /// Setea las estadísticas del enemigo y cambia la animación según el jefe actual.
    /// </summary>
    /// <param name="nuevaVida"></param>
    /// <param name="nuevoDaño"></param>
    /// <param name="nuevosTurnos"></param>
    /// <param name="jefeID">ID para el parámetro Animator que decide la animación</param>
    /// <param name="sprite">Sprite que se muestra en la UI</param>
    public void SetStats(int nuevaVida, int nuevoDaño, int nuevosTurnos, int jefeID, Sprite sprite)
    {
        vidaMaxima = nuevaVida;
        vidaActual = nuevaVida;
        daño = nuevoDaño;
        turnosParaAtacar = nuevosTurnos;
        turnosRestantes = nuevosTurnos;

        if (animator != null)
        {
            animator.SetInteger("JefeID", jefeID);
            Debug.Log($"[EnemyController] Parámetro JefeID seteado a {jefeID}.");
        }

        if (sprite != null && enemyImageUI != null)
        {
            enemyImageUI.sprite = sprite;
            Debug.Log("[EnemyController] Sprite asignado correctamente.");
        }

        ActualizarUICompleta();
        estaActivo = true;
    }

    public void OnPlayerTurnEnd()
    {
        if (!estaActivo) return;

        if (turnosConVeneno > 0)
        {
            AplicarDanoDesdeCombo(dañoVenenoPorTurno);
            turnosConVeneno--;
            if (turnosConVeneno <= 0)
            {
                dañoVenenoPorTurno = 0;
            }
        }

        if (turnosConReduccion > 0)
        {
            turnosConReduccion--;
            if (turnosConReduccion <= 0)
            {
                reduccionDaño = 0;
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
        }

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

        ActualizarTextoVida();

        if (vidaActual == 0)
        {
            estaActivo = false;
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.SubirDeNivel();
            }
        }
    }

    public void AplicarVeneno(int dañoPorTurno, int turnos)
    {
        if (!estaActivo) return;

        dañoVenenoPorTurno = dañoPorTurno;
        turnosConVeneno = turnos;
    }

    public void AplicarDebuffDaño(int cantidad, int turnos)
    {
        if (!estaActivo) return;

        reduccionDaño = cantidad;
        turnosConReduccion = turnos;
    }

    public void RetrasarProximoAtaque()
    {
        if (!estaActivo) return;

        turnosRestantes++;
        ActualizarTextoTurnos();
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
