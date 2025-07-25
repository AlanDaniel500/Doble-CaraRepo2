using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboAbyss : MonoBehaviour, ICombo, IComboConEfecto
{
    public string Nombre => "Abismo";

    [SerializeField] private int prioridad = 6;
    public int Prioridad => prioridad;

    [SerializeField] private AudioClip comboSFX; 

    [SerializeField] private int dañoBase = 250;

    [Header("Efecto Oscuridad")]
    [SerializeField] private int reduccionDaño = 30;
    [SerializeField] private int turnosDeDebuff = 2;

    public bool CheckCombo(List<CardData> cartas)
    {
        int oscuridadCount = 0;

        foreach (var carta in cartas)
        {
            if (carta.cardType == CardData.CardType.Oscuridad)
                oscuridadCount++;
        }

        return oscuridadCount >= 4;
    }

    public int CalcularDaño(List<CardData> cartas)
    {
        int sumaValores = 0;

        foreach (var carta in cartas)
        {
            sumaValores += carta.cardNumber;
        }

        int totalDaño = dañoBase + sumaValores;
        Debug.Log($"Combo Abismo activado Daño base: {dañoBase} + suma cartas: {sumaValores} = {totalDaño}");

        return totalDaño;
    }

    public void PlaySFX()
    {
        if (comboSFX != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("Abismo");
        }
        else
        {
        Debug.LogWarning("Falta el sonido o AudioManager para el combo: " + Nombre);
        }
    }


    public void AplicarEfecto(List<CardData> cartas, Player player)
    {
        EnemyController enemy = FindFirstObjectByType<EnemyController>();
        if (enemy != null)
        {
            enemy.AplicarDebuffDaño(reduccionDaño, turnosDeDebuff);
        }
        else
        {
            Debug.LogWarning("No se encontró EnemyController para aplicar debuff de oscuridad.");
        }
    }
}
