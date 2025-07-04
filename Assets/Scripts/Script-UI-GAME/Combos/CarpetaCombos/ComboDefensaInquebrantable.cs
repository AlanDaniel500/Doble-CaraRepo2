using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboDefensaInquebrantable : MonoBehaviour, ICombo, IComboConEfecto
{
    public string Nombre => "Defensa Inquebrantable";

    [SerializeField] private int prioridad = 6;
    public int Prioridad => prioridad;

    [SerializeField] private int dañoBase = 150;

    private int ultimoDaño = 0;

    public bool CheckCombo(List<CardData> cartas)
    {
        int luzCount = 0;

        foreach (var carta in cartas)
        {
            if (carta.cardType == CardData.CardType.Luz)
                luzCount++;
        }

        return luzCount >= 4;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        int suma = 0;

        foreach (var carta in cartas)
        {
            suma += carta.cardNumber;
        }

        ultimoDaño = dañoBase + suma;
        Debug.Log($"Combo Defensa Inquebrantable activado ⚡ Daño base: {dañoBase} + suma cartas: {suma} = {ultimoDaño}");
        return ultimoDaño;
    }

    public void AplicarEfecto(List<CardData> cartas, Player player)
    {
        EnemyController enemy = FindFirstObjectByType<EnemyController>();

        if (enemy != null)
        {
            enemy.RetrasarProximoAtaque(); // <- este método lo agregaste antes
            Debug.Log($"Combo Defensa Inquebrantable: se retrasó el ataque enemigo 1 turno 🛡️");
        }
        else
        {
            Debug.LogWarning("EnemyController no encontrado para aplicar efecto de Defensa Inquebrantable");
        }
    }
}
