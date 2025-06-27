using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ManagerCombos : MonoBehaviour
{
    [SerializeField] private List<CardData> cartasEnMano;

    // Referencia al Player (la podés setear desde el inspector)
    [SerializeField] private Player player;

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

    // Ejecuta el combo: calcula daño y aplica efectos si tiene
    public int EjecutarCombo(List<CardData> cartas)
    {
        ICombo combo = RevisarMejorCombo(cartas);

        if (combo != null)
        {
            int daño = combo.CalcularDaño(cartas);

            // Verifica si tiene efecto especial y pasa player
            if (combo is IComboConEfecto comboConEfecto)
            {
                comboConEfecto.AplicarEfecto(cartas, player);
            }

            return daño;
        }

        return 0;
    }

    public void SetCartas(List<CardData> nuevasCartas)
    {
        cartasEnMano = nuevasCartas;
    }
}
