using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ManagerCombos : MonoBehaviour
{
    [SerializeField] private List<CardData> cartasEnMano;

    private List<ICombo> combos = new List<ICombo>();

    private void Start()
    {
        combos.AddRange(GetComponentsInChildren<ICombo>());
    }

    public ICombo RevisarMejorCombo(List<CardData> cartas)
    {
        ICombo mejorCombo = null;
        int mayorPrioridad = int.MinValue;

        foreach (ICombo combo in combos)
        {
            if (combo.CheckCombo(cartas) && combo.Prioridad > mayorPrioridad)
            {
                mejorCombo = combo;
                mayorPrioridad = combo.Prioridad;
            }
        }

        return mejorCombo;
    }

    public void SetCartas(List<CardData> nuevasCartas)
    {
        cartasEnMano = nuevasCartas;
    }
}
