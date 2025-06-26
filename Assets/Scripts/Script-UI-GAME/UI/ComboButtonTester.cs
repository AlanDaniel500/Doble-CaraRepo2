using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class ComboButtonTester : MonoBehaviour
{
    [SerializeField] private EnemyTurnCounter enemyTurnCounter;
    [SerializeField] private CardSpawner cardSpawner;
    [SerializeField] private ManagerCombos managerCombos;
    [SerializeField] private RecibeDanoEnemigo enemigo;
    [SerializeField] private ComboUIManager comboUIManager;

    public void OnComboPressed()
    {
        if (cardSpawner == null || managerCombos == null || enemigo == null || comboUIManager == null)
        {
            Debug.LogError("Faltan referencias necesarias en ComboButtonTester");
            return;
        }

        if (!cardSpawner.HayCartasSeleccionadas())
        {
            Debug.Log("Tenés que seleccionar mínimo una carta");
            return;
        }

        List<CardData> cartasSeleccionadas = cardSpawner.ObtenerCartasSeleccionadas();
        if (cartasSeleccionadas == null || cartasSeleccionadas.Count == 0)
        {
            Debug.LogWarning("No hay cartas seleccionadas");
            return;
        }

        ICombo comboActivo = managerCombos.RevisarMejorCombo(cartasSeleccionadas);
        if (comboActivo == null)
        {
            Debug.LogWarning("No se encontró ningún combo válido");
            return;
        }

        int dañoAplicado = comboActivo.CalcularDaño(cartasSeleccionadas);
        Debug.Log($"Combo detectado: {comboActivo.Nombre}, daño: {dañoAplicado}");

     
        comboUIManager.MostrarCombo(comboActivo.Nombre, dañoAplicado);

        enemigo.AplicarDanoDesdeCombo(dañoAplicado);

        cardSpawner.EliminarCartasSeleccionadas();
        CardSelector.ReiniciarContador();

        enemyTurnCounter.OnPlayerTurnEnd();
    }
}
