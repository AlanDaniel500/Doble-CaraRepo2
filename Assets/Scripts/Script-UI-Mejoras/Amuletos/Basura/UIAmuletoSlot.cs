using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIAmuletoSlot : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private Image icono;
    [SerializeField] private TMP_Text nombreTexto;
    [SerializeField] private TMP_Text descripcionTexto;
    [SerializeField] private Button boton;

    private int index;
    private System.Action<int> onSeleccionado;

    public void Configurar(AmuletoData data, int i, System.Action<int> onElegido)
    {
        icono.sprite = data.icono;
        nombreTexto.text = data.nombre;
        descripcionTexto.text = data.descripcion;

        index = i;
        onSeleccionado = onElegido;

        boton.onClick.RemoveAllListeners();
        boton.onClick.AddListener(() => onSeleccionado?.Invoke(index));
    }
}
