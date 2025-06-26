using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboAbyss : MonoBehaviour, ICombo
{
    public string Nombre => "Abismo";
    public int Prioridad => 6;

    public bool CheckCombo(List<CardData> cartas)
    {
        int oscuridadCount = 0;

        foreach (var carta in cartas)
        {
            if (carta.cardType == CardData.CardType.Oscuridad)
                oscuridadCount++;
        }

        return oscuridadCount >= 4;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        Debug.Log("Combo Abismo activado ? 250 de daño");
        return 250;
    }
}

