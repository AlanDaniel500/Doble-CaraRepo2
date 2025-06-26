using UnityEngine;
using TMPro;

public class EnemyTurnCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private int maxTurns = 2;

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
            TriggerEnemyAction();
            ResetCounter();
        }

        UpdateText();
    }

    private void TriggerEnemyAction()
    {
        Debug.Log("¡El enemigo ataca!");

        if (ataqueEnemigo != null)
        {
            ataqueEnemigo.EjecutarAtaque();
        }
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
