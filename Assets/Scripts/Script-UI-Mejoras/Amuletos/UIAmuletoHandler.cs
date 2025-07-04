using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIAmuletoHandler : MonoBehaviour
{
    [Header("Referencias UI (tres slots de amuletos)")]
    public List<TextMeshProUGUI> titulos;
    public List<Button> botonesAmuletos;
    public List<TextMeshProUGUI> descripciones;

    public Button confirmarBoton;

    private List<AmuletoListaEfectos> amuletosMostrados;
    private int indexSeleccionado = -1;

    private void Awake()
    {
        confirmarBoton.onClick.AddListener(ConfirmarSeleccion);
    }

    public void MostrarAmuletos(List<AmuletoListaEfectos> opciones)
    {
        amuletosMostrados = opciones;

        for (int i = 0; i < 3; i++)
        {
            titulos[i].text = opciones[i].nombre;
            botonesAmuletos[i].image.sprite = opciones[i].imagen;
            descripciones[i].text = opciones[i].descripcion;

            int idx = i;
            botonesAmuletos[i].onClick.RemoveAllListeners();
            botonesAmuletos[i].onClick.AddListener(() => Seleccionar(idx));
        }

        confirmarBoton.gameObject.SetActive(false);
        indexSeleccionado = -1;
    }

    void Seleccionar(int index)
    {
        indexSeleccionado = index;
        confirmarBoton.gameObject.SetActive(true);
        Debug.Log("[UIAmuletoHandler] Amuleto seleccionado: " + amuletosMostrados[index].nombre);
    }

    public AmuletoListaEfectos GetAmuletoSeleccionado()
    {
        return indexSeleccionado >= 0 ? amuletosMostrados[indexSeleccionado] : null;
    }

    void ConfirmarSeleccion()
    {
        AmuletoListaEfectos seleccionado = GetAmuletoSeleccionado();

        if (seleccionado == null)
        {
            Debug.Log("[UIAmuletoHandler] No hay amuleto seleccionado.");
            return;
        }

        Debug.Log("[UIAmuletoHandler] Amuleto confirmado: " + seleccionado.nombre);

        if (AmuletoEffectManager.Instance != null)
        {
            AmuletoEffectManager.Instance.AplicarEfectos(seleccionado);
        }

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ContinuarCombate(); // 🔁 Volver a escena de combate
        }
        else
        {
            Debug.LogWarning("[UIAmuletoHandler] No se encontró LevelManager.");
        }
    }
}
