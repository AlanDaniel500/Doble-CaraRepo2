using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboGemelos : MonoBehaviour, ICombo
{
    public string Nombre => "Gemelos";
    public int Prioridad => 2;

    [SerializeField] private int dañoBase = 0; // Nuevo campo configurable desde el Inspector

    public bool CheckCombo(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count < 2)
            return false;

        // Contar cuántas cartas hay de cada número
        var conteo = new Dictionary<int, int>();
        foreach (var carta in cartas)
        {
            if (!conteo.ContainsKey(carta.cardNumber))
                conteo[carta.cardNumber] = 0;
            conteo[carta.cardNumber]++;
        }

        // Verificar que haya al menos un par
        foreach (var cantidad in conteo.Values)
        {
            if (cantidad >= 2)
                return true;
        }

        return false;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count < 2)
            return 0;

        // Contar y guardar las cartas por número
        var cartasPorNumero = new Dictionary<int, List<CardData>>();
        foreach (var carta in cartas)
        {
            if (!cartasPorNumero.ContainsKey(carta.cardNumber))
                cartasPorNumero[carta.cardNumber] = new List<CardData>();
            cartasPorNumero[carta.cardNumber].Add(carta);
        }

        int dañoMaximo = 0;

        foreach (var grupo in cartasPorNumero.Values)
        {
            if (grupo.Count >= 2)
            {
                int sumaPar = grupo[0].cardNumber + grupo[1].cardNumber;
                if (sumaPar > dañoMaximo)
                    dañoMaximo = sumaPar;
            }
        }

        return dañoBase + dañoMaximo; // Suma del daño base + valor del par más fuerte
    }
}
