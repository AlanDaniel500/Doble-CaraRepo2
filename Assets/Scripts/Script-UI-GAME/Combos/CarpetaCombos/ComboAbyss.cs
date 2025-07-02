using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboAbyss : MonoBehaviour, ICombo
{
    public string Nombre => "Abismo";
    
    [SerializeField] private int prioridad = 6; // Editable desde inspector
    public int Prioridad => prioridad;


    [SerializeField] private int dañoBase = 250; // Editable desde el inspector

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
        int sumaValores = 0;

        foreach (var carta in cartas)
        {
            sumaValores += carta.cardNumber;
        }

        int totalDaño = dañoBase + sumaValores;

        Debug.Log($"Combo Abismo activado Daño base: {dañoBase} + suma cartas: {sumaValores} = {totalDaño}");

        return totalDaño;
    }
}

