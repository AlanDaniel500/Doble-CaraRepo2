using TMPro;
using UnityEngine;

public class ComboUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI comboTexto;

    public void MostrarCombo(string nombreCombo, int daño)
    {
        if (comboTexto == null) return;

        if (string.IsNullOrEmpty(nombreCombo) || daño <= 0)
        {
            comboTexto.text = "No hay combo";
        }
        else
        {
            comboTexto.text = $"Combo: {nombreCombo} - Daño: {daño}";
        }
    }

    public void Limpiar()
    {
        if (comboTexto != null)
            comboTexto.text = "";
    }
}

