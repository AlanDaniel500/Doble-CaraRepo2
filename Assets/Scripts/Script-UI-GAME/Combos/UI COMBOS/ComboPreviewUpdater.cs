using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class ComboPreviewUpdater : MonoBehaviour
{
    [SerializeField] private ComboUIManager comboUIManager;
    [SerializeField] private ManagerCombos managerCombos;
    [SerializeField] private CardSpawner cardSpawner;

    private List<CardData> cartasPrevias = new List<CardData>();

    private void Update()
    {
        if (cardSpawner == null || comboUIManager == null || managerCombos == null)
            return;

        List<CardData> cartasActuales = cardSpawner.ObtenerCartasSeleccionadas();
        if (cartasActuales == null)
        {
            comboUIManager.Limpiar();
            return;
        }

        // Evitar actualizar si no hubo cambios
        if (cartasActuales.Count == cartasPrevias.Count)
        {
            bool iguales = true;
            for (int i = 0; i < cartasActuales.Count; i++)
            {
                if (cartasActuales[i] != cartasPrevias[i])
                {
                    iguales = false;
                    break;
                }
            }

            if (iguales) return;
        }

        cartasPrevias = new List<CardData>(cartasActuales);

        if (cartasActuales.Count >= 1)
        {
            ICombo mejorCombo = managerCombos.RevisarMejorCombo(cartasActuales);
            if (mejorCombo != null)
            {
                int daño = mejorCombo.CalcularDaño(cartasActuales);
                comboUIManager.MostrarCombo(mejorCombo.Nombre, daño);
            }
            else
            {
                comboUIManager.MostrarCombo("Sin combo", 0);
            }
        }
        else
        {
            comboUIManager.Limpiar();
        }
    }
}
