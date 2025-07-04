using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboPandemonium : MonoBehaviour, ICombo, IComboConEfecto
{
    public string Nombre => "Pandemonio";

    [SerializeField] private int prioridad = 6;
    public int Prioridad => prioridad;

    [SerializeField] private int dañoBase = 100;

    private int ultimoDaño;

    public bool CheckCombo(List<CardData> cartas)
    {
        int muerteCount = 0;

        foreach (var carta in cartas)
        {
            if (carta.cardType == CardData.CardType.Muerte)
                muerteCount++;
        }

        return muerteCount >= 4;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        int sumaValores = 0;

        foreach (var carta in cartas)
        {
            sumaValores += carta.cardNumber;
        }

        ultimoDaño = dañoBase + sumaValores;

        Debug.Log($"Combo Pandemonio activado ☠️ Daño base: {dañoBase} + suma cartas: {sumaValores} = {ultimoDaño}");

        return ultimoDaño;
    }

    public void AplicarEfecto(List<CardData> cartas, Player player)
    {
        EnemyController enemy = FindFirstObjectByType<EnemyController>();

        if (enemy != null)
        {
            int venenoPorTurno = 10;  // o lo podés ajustar desde el inspector
            int turnos = 3;

            enemy.AplicarVeneno(venenoPorTurno, turnos);
            Debug.Log($"Efecto Pandemonio: Envenenado por {turnos} turnos, {venenoPorTurno} de daño por turno ☣️");
        }
        else
        {
            Debug.LogWarning("EnemyController no encontrado para aplicar veneno en Pandemonium");
        }
    }
}
