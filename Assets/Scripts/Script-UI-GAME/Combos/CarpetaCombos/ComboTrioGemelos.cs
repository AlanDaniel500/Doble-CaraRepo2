using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboTrioGemelos : MonoBehaviour, ICombo
{
    public string Nombre => "Trío Gemelos";

    [SerializeField] private int prioridad = 7;

    [SerializeField] private AudioClip comboSFX;
    public int Prioridad => prioridad;

    [SerializeField] private int dañoBase = 65;

    public bool CheckCombo(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count != 6)
            return false;

        Dictionary<int, int> conteoPorNumero = new Dictionary<int, int>();

        foreach (var carta in cartas)
        {
            if (!conteoPorNumero.ContainsKey(carta.cardNumber))
                conteoPorNumero[carta.cardNumber] = 0;
            conteoPorNumero[carta.cardNumber]++;
        }

        int pares = 0;

        foreach (var cantidad in conteoPorNumero.Values)
        {
            if (cantidad == 2)
                pares++;
        }

        return pares == 3;
    }

    public void PlaySFX()
    {
        if (comboSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Trio Gemelos");
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
        Debug.Log($"Combo Trío Gemelos activado: daño base {dañoBase} + suma {suma} = {totalDaño}");
        return totalDaño;
    }
}
