using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboCerberiders : MonoBehaviour, ICombo
{
    public string Nombre => "Cerberiders";

    [SerializeField] private int prioridad = 8;

    [SerializeField] private AudioClip comboSFX;
    public int Prioridad => prioridad;

    [SerializeField] private int dañoBase = 80;

    public bool CheckCombo(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count != 7)
            return false;

        Dictionary<int, int> conteoPorNumero = new Dictionary<int, int>();

        foreach (var carta in cartas)
        {
            if (!conteoPorNumero.ContainsKey(carta.cardNumber))
                conteoPorNumero[carta.cardNumber] = 0;

            conteoPorNumero[carta.cardNumber]++;
        }

        bool tieneTrio = false;
        bool tienePoker = false;

        foreach (var cantidad in conteoPorNumero.Values)
        {
            if (cantidad == 3)
                tieneTrio = true;
            else if (cantidad == 4)
                tienePoker = true;
        }

        return tieneTrio && tienePoker;
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
        Debug.Log($"Combo Cerberiders activado: daño base {dañoBase} + suma {suma} = {totalDaño}");
        return totalDaño;
    }
}
