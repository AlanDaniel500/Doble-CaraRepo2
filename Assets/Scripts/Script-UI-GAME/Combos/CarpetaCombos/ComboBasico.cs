using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboBasico : MonoBehaviour, ICombo
{
    public string Nombre => "Básico";
    public int Prioridad => 1;

    public bool CheckCombo(List<CardData> cartas)
    {
        return cartas != null && cartas.Count > 0;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        CardData cartaMayor = ObtenerCartaDeMayorValor(cartas);
        return cartaMayor != null ? cartaMayor.cardNumber : 0;
    }

    // Cambiado de private a public para que pueda ser usado externamente
    public CardData ObtenerCartaDeMayorValor(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count == 0) return null;

        CardData cartaMayor = cartas[0];
        foreach (var carta in cartas)
        {
            if (carta.cardNumber > cartaMayor.cardNumber)
                cartaMayor = carta;
        }
        return cartaMayor;
    }
}

