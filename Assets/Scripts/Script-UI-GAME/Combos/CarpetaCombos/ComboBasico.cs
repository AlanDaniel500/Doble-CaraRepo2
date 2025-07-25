using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboBasico : MonoBehaviour, ICombo
{
    public string Nombre => "Básico";
    
    [SerializeField] private int prioridad = 1; // Editable desde inspector
    public int Prioridad => prioridad;

    [SerializeField] private AudioClip comboSFX;

    [SerializeField] private int dañoBase = 0; // Editable desde el Inspector

    public bool CheckCombo(List<CardData> cartas)
    {
        return cartas != null && cartas.Count >= 1; // ✅ CORREGIDO: acepta 1 o más cartas
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        CardData cartaMayor = ObtenerCartaDeMayorValor(cartas);
        int dañoCarta = cartaMayor != null ? cartaMayor.cardNumber : 0;
        return dañoBase + dañoCarta;
    }

    public void PlaySFX()
    {
        if (comboSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Basico");
        }
        else
        {
            Debug.LogWarning("Falta el sonido o AudioManager para el combo: " + Nombre);
        }
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

