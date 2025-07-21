using UnityEngine;
using System.Collections.Generic;
using CardSystem;
using System.Collections;

public class ComboButtonTester : MonoBehaviour
{
    [SerializeField] private CardSpawner cardSpawner;
    [SerializeField] private ManagerCombos managerCombos;
    [SerializeField] private EnemyController enemyController; // Ahora manejamos todo acá
    [SerializeField] private ComboUIManager comboUIManager;
    [SerializeField] private Animator playButtonAnimator;


    public void OnComboPressed()
    {
        if (cardSpawner == null || managerCombos == null || enemyController == null || comboUIManager == null)
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

        int dañoAplicado = managerCombos.EjecutarCombo(cartasSeleccionadas);

        if (dañoAplicado == 0)
        {
            Debug.LogWarning("No se encontró ningún combo válido");
            return;
        }

        if (playButtonAnimator != null)
        {
            playButtonAnimator.SetTrigger("Pressed");
        }



        ICombo comboActivo = managerCombos.RevisarMejorCombo(cartasSeleccionadas);

        comboUIManager.MostrarCombo(comboActivo.Nombre, dañoAplicado);

        // Aplicar daño usando EnemyController
        enemyController.AplicarDanoDesdeCombo(dañoAplicado);

        cardSpawner.EliminarCartasSeleccionadas();
        CardSelector.ReiniciarContador();

        // Ahora le avisamos directamente al EnemyController que terminó el turno del jugador
        enemyController.OnPlayerTurnEnd();
    }
}
