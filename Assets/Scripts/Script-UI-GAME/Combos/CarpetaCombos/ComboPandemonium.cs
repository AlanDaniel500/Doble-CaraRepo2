using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboPandemonium : MonoBehaviour, ICombo
{
    public string Nombre => "Pandemonio";
    public int Prioridad => 5;

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
        Debug.Log("Combo Pandemonio activado ? 100 de daño");
        return 100;
    }
}

