using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboBigPain : MonoBehaviour, ICombo, IComboConEfecto
{
    public string Nombre => "Gran Dolor";

    [SerializeField] private int prioridad = 6; // Editable desde el inspector

    [SerializeField] private AudioClip comboSFX; 
    public int Prioridad => prioridad;

    [SerializeField] private int dañoBase = 75;

    private int ultimoDañoCalculado; // Para usarlo en AplicarEfecto

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

    public void PlaySFX()
    {
        if (comboSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Gran dolor");
        }
        else
        {
            Debug.LogWarning("Falta el sonido o AudioManager para el combo: " + Nombre);
        }
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        int sumaValores = 0;

        foreach (var carta in cartas)
        {
            sumaValores += carta.cardNumber;
        }

        int totalDaño = dañoBase + sumaValores;
        ultimoDañoCalculado = totalDaño; // Lo guardamos para usarlo luego en el efecto

        Debug.Log($"Combo Gran Dolor activado 🩸 Daño base: {dañoBase} + suma cartas: {sumaValores} = {totalDaño}");

        return totalDaño;
    }

    // Cambié para recibir Player como parámetro
    public void AplicarEfecto(List<CardData> cartas, Player player)
    {
        if (player != null)
        {
            player.Curar(ultimoDañoCalculado);
            Debug.Log($"Robo de vida: se curó {ultimoDañoCalculado} puntos 💉");
        }
        else
        {
            Debug.LogWarning("Player es null en AplicarEfecto de ComboBigPain");
        }
    }
}
