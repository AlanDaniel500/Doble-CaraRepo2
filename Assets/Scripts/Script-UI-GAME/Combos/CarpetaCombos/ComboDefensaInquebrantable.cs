using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboDefensaInquebrantable : MonoBehaviour, ICombo
{
    public string Nombre => "Defensa Inquebrantable";
    
    [SerializeField] private int prioridad = 6; // Editable desde inspector
    public int Prioridad => prioridad;


    [SerializeField] private int dañoBase = 150; // Editable desde el editor

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
        int sumaValores = 0;

        foreach (var carta in cartas)
        {
            sumaValores += carta.cardNumber;
        }

        int totalDaño = dañoBase + sumaValores;
        Debug.Log($"Combo Defensa Inquebrantable activado ⚡ Daño base: {dañoBase} + suma cartas: {sumaValores} = {totalDaño}");

        return totalDaño;
    }
}
