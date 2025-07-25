using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboEcoGemelo : MonoBehaviour, ICombo
{
    public string Nombre => "Eco Gemelo";

    [SerializeField] private int prioridad = 6;

    [SerializeField] private AudioClip comboSFX;

    public int Prioridad => prioridad;

    [SerializeField] private int dañoBase = 50;

    public bool CheckCombo(List<CardData> cartas)
    {
        Dictionary<int, int> conteoPorNumero = new Dictionary<int, int>();

        foreach (var carta in cartas)
        {
            if (!conteoPorNumero.ContainsKey(carta.cardNumber))
                conteoPorNumero[carta.cardNumber] = 0;

            conteoPorNumero[carta.cardNumber]++;
        }

        int cantidadPares = 0;

        foreach (var cantidad in conteoPorNumero.Values)
        {
            if (cantidad >= 2)
                cantidadPares++;
        }

        return cantidadPares >= 2;
    }

    public void PlaySFX()
    {
        if (comboSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Eco Gemelos");
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
        Debug.Log($"Combo Eco Gemelo activado: daño base {dañoBase} + suma {suma} = {totalDaño}");
        return totalDaño;
    }
}
