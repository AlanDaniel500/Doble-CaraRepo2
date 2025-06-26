using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboCerbero : MonoBehaviour, ICombo
{
    public string Nombre => "Cerbero";
    public int Prioridad => 3;

    public bool CheckCombo(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count < 3) return false;

        var conteo = new Dictionary<int, int>();

        foreach (var carta in cartas)
        {
            if (!conteo.ContainsKey(carta.cardNumber))
                conteo[carta.cardNumber] = 0;

            conteo[carta.cardNumber]++;
        }

        foreach (var cantidad in conteo.Values)
        {
            if (cantidad >= 3) return true;
        }

        return false;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count < 3) return 0;

        var grupos = new Dictionary<int, List<CardData>>();

        foreach (var carta in cartas)
        {
            if (!grupos.ContainsKey(carta.cardNumber))
                grupos[carta.cardNumber] = new List<CardData>();

            grupos[carta.cardNumber].Add(carta);
        }

        int maxDanio = 0;

        foreach (var grupo in grupos.Values)
        {
            if (grupo.Count >= 3)
            {
                int suma = 0;
                for (int i = 0; i < 3; i++)
                {
                    suma += grupo[i].cardNumber;
                }

                Debug.Log($"Combo Cerbero activado con tres {grupo[0].cardNumber}, daño: {suma}");

                if (suma > maxDanio)
                    maxDanio = suma;
            }
        }

        return maxDanio;
    }
}
