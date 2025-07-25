using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboDobleRiders : MonoBehaviour, ICombo
{
    public string Nombre => "Doble Riders";

    [SerializeField] private int prioridad = 9;

    [SerializeField] private AudioClip comboSFX;

    public int Prioridad => prioridad;

    [SerializeField] private int dañoBase = 90;

    public bool CheckCombo(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count != 8)
            return false;

        Dictionary<int, int> conteoPorNumero = new Dictionary<int, int>();

        foreach (var carta in cartas)
        {
            if (!conteoPorNumero.ContainsKey(carta.cardNumber))
                conteoPorNumero[carta.cardNumber] = 0;

            conteoPorNumero[carta.cardNumber]++;
        }

        int cantidadPokers = 0;

        foreach (var cantidad in conteoPorNumero.Values)
        {
            if (cantidad == 4)
                cantidadPokers++;
        }

        return cantidadPokers == 2;
    }

    public void PlaySFX()
    {
        if (comboSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Rider");
        }
        else
        {
            Debug.LogWarning("Falta el sonido o AudioManager para el combo: " + Nombre);
        }
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        int suma = 0;
        foreach (var carta in cartas)
        {
            suma += carta.cardNumber;
        }

        int totalDaño = dañoBase + suma;
        Debug.Log($"Combo Doble Riders activado: daño base {dañoBase} + suma {suma} = {totalDaño}");
        return totalDaño;
    }
}
