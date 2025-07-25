using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboReinoDeLasLlamas : MonoBehaviour, ICombo
{
    public string Nombre => "Reino de las Llamas";

    [SerializeField] private int prioridad = 10;

    [SerializeField] private AudioClip comboSFX;

    public int Prioridad => prioridad;

    [SerializeField] private int dañoBase = 700;

    public bool CheckCombo(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count != 7)
            return false;

        // Verificar que todas las cartas tengan el mismo tipo (cardType)
        var tipoReferencia = cartas[0].cardType;

        foreach (var carta in cartas)
        {
            if (carta.cardType != tipoReferencia)
                return false;
        }

        // Verificar que los valores sean únicos
        HashSet<int> numeros = new HashSet<int>();
        foreach (var carta in cartas)
        {
            numeros.Add(carta.cardNumber);
        }

        if (numeros.Count != 7)
            return false;

        List<int> listaOrdenada = new List<int>(numeros);
        listaOrdenada.Sort();

        // Comprobar si los valores forman una escalera válida
        int[][] escalerasValidas = new int[][]
        {
            new int[] {1,2,3,4,5,6,7},
            new int[] {2,3,4,5,6,7,8},
            new int[] {3,4,5,6,7,8,9},
            new int[] {4,5,6,7,8,9,10}
        };

        foreach (var escalera in escalerasValidas)
        {
            bool coincide = true;
            for (int i = 0; i < 7; i++)
            {
                if (listaOrdenada[i] != escalera[i])
                {
                    coincide = false;
                    break;
                }
            }

            if (coincide)
                return true;
        }

        return false;
    }

    public void PlaySFX()
    {
        if (comboSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Reino de las llamas");
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
        Debug.Log($"Combo Reino de las Llamas activado: daño base {dañoBase} + suma {suma} = {totalDaño}");
        return totalDaño;
    }
}
