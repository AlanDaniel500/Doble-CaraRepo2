using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboBigPain : MonoBehaviour, ICombo
{
    public string Nombre => "Gran Dolor";
    public int Prioridad => 5; // Puede ser mayor que los otros si querés que tenga más peso

    public bool CheckCombo(List<CardData> cartas)
    {
        int sangreCount = 0;

        foreach (var carta in cartas)
        {
            if (carta.cardType == CardData.CardType.Sangre)
                sangreCount++;
        }

        return sangreCount >= 4;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        Debug.Log("Combo Gran Dolor activado ? 75 de daño");
        return 75;
    }
}

