using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyTurnCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private int maxTurns = 2;

    [Header("Delays para mostrar el 0 y ataque")]
    [SerializeField] private float delayAntesDeAtacar = 1f;  // Delay antes del ataque (en segundos)
    [SerializeField] private float delayDespuesDeAtacar = 1f; // Delay después del ataque (en segundos)

    private int currentTurn;
    private AtaqueEnemigo ataqueEnemigo;

    void Start()
    {
        ataqueEnemigo = FindFirstObjectByType<AtaqueEnemigo>();
        ResetCounter();
    }

    public void OnPlayerTurnEnd()
    {
        currentTurn--;

        if (currentTurn <= 0)
        {
            // Mostrar 0 inmediatamente mientras se espera el ataque
            turnText.text = "0";
            StartCoroutine(TriggerEnemyActionConDelays());
        }
        else
        {
            UpdateText();
        }
    }

    private IEnumerator TriggerEnemyActionConDelays()
    {
        // Espera antes de atacar (mantiene el 0)
        yield return new WaitForSeconds(delayAntesDeAtacar);

        Debug.Log("¡El enemigo ataca!");

        if (ataqueEnemigo != null)
        {
            ataqueEnemigo.EjecutarAtaque();
        }

        // Mantener 0 tras el ataque un tiempo más
        yield return new WaitForSeconds(delayDespuesDeAtacar);

        ResetCounter(); // Después de los delays, reiniciar contador
    }

    private void ResetCounter()
    {
        currentTurn = maxTurns;
        UpdateText();
    }

    private void UpdateText()
    {
        turnText.text = currentTurn.ToString();
    }

    public void SetMaxTurns(int newMax)
    {
        maxTurns = newMax;
        ResetCounter();
    }
}
