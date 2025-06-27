using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboBasico : MonoBehaviour, ICombo
{
    public string Nombre => "Básico";
    public int Prioridad => 1;

    [SerializeField] private int dañoBase = 0; // Nuevo campo editable desde el Inspector

    public bool CheckCombo(List<CardData> cartas)
    {
        return cartas != null && cartas.Count > 0;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        CardData cartaMayor = ObtenerCartaDeMayorValor(cartas);
        int dañoCarta = cartaMayor != null ? cartaMayor.cardNumber : 0;
        return dañoBase + dañoCarta; // Suma del daño base + valor de la carta mayor
    }

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
