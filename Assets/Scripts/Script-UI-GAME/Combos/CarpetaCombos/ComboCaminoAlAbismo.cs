using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboCaminoAlAbismo : MonoBehaviour, ICombo
{
    public string Nombre => "Camino al Abismo";

    [SerializeField] private int prioridad = 10;
    public int Prioridad => prioridad;

    [SerializeField] private AudioClip comboSFX;

    [SerializeField] private int dañoBase = 600;

    public bool CheckCombo(List<CardData> cartas)
    {
        if (cartas == null || cartas.Count != 7)
            return false;

        HashSet<int> numeros = new HashSet<int>();

        foreach (var carta in cartas)
        {
            numeros.Add(carta.cardNumber);
        }

        // Si hay duplicados, no puede ser una escalera
        if (numeros.Count != 7)
            return false;

        List<int> listaOrdenada = new List<int>(numeros);
        listaOrdenada.Sort();

        // Verificamos las posibles escaleras válidas de 7 números consecutivos
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
            AudioManager.Instance.PlaySFX("Camino al abismo");
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
        Debug.Log($"Combo Camino al Abismo activado: daño base {dañoBase} + suma {suma} = {totalDaño}");
        return totalDaño;
    }
}
