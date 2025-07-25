using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ManejoPaginasUI : MonoBehaviour
{
    [Header("Lista de páginas del libro (en orden)")]
    [SerializeField] private List<InfoCardUIData> paginas;

    [Header("Referencias a la UI - Lado Izquierdo")]
    [SerializeField] private TextMeshProUGUI tituloIzq;
    [SerializeField] private Image imagenIzq;
    [SerializeField] private TextMeshProUGUI descripcionIzq;
    [SerializeField] private TextMeshProUGUI dañoIzq;
    [SerializeField] private TextMeshProUGUI efectoExtraIzq;

    [Header("Referencias a la UI - Lado Derecho")]
    [SerializeField] private TextMeshProUGUI tituloDer;
    [SerializeField] private Image imagenDer;
    [SerializeField] private TextMeshProUGUI descripcionDer;
    [SerializeField] private TextMeshProUGUI dañoDer;
    [SerializeField] private TextMeshProUGUI efectoExtraDer;

    [Header("Botones")]
    [SerializeField] private Button botonAnterior;
    [SerializeField] private Button botonSiguiente;

    private int indiceActual = 0;

    private void Start()
    {
        ActualizarPaginas();
    }

    public void PaginaSiguiente()
    {
        if (indiceActual + 2 < paginas.Count)
        {
            indiceActual += 2;
        }
        else
        {
            // Si no hay más páginas adelante, volver al principio
            indiceActual = 0;
        }
        ActualizarPaginas();
    }

    public void PaginaAnterior()
    {
        if (indiceActual - 2 >= 0)
        {
            indiceActual -= 2;
        }
        else
        {
            // Si ya estamos en la primera página, saltamos al final
            if (paginas.Count % 2 == 0)
            {
                // Última dupla si la cantidad es par
                indiceActual = paginas.Count - 2;
            }
            else
            {
                // Última página sola si es impar
                indiceActual = paginas.Count - 1;
            }
        }
        ActualizarPaginas();
    }

    private void ActualizarPaginas()
    {
        // Lado Izquierdo
        if (indiceActual < paginas.Count)
        {
            MostrarPagina(paginas[indiceActual], tituloIzq, imagenIzq, descripcionIzq, dañoIzq, efectoExtraIzq);
        }
        else
        {
            LimpiarPagina(tituloIzq, imagenIzq, descripcionIzq, dañoIzq, efectoExtraIzq);
        }

        // Lado Derecho
        if (indiceActual + 1 < paginas.Count)
        {
            MostrarPagina(paginas[indiceActual + 1], tituloDer, imagenDer, descripcionDer, dañoDer, efectoExtraDer);
        }
        else
        {
            LimpiarPagina(tituloDer, imagenDer, descripcionDer, dañoDer, efectoExtraDer);
        }

        // Actualizar estado de botones (opcionales, pueden seguir activos)
        botonAnterior.interactable = true;
        botonSiguiente.interactable = true;
    }

    private void MostrarPagina(InfoCardUIData data, TextMeshProUGUI titulo, Image imagen, TextMeshProUGUI descripcion, TextMeshProUGUI daño, TextMeshProUGUI efectoExtra)
    {
        titulo.text = data.titulo;
        imagen.sprite = data.imagen;
        imagen.enabled = data.imagen != null;

        descripcion.text = data.descripcion;
        daño.text = $"Daño: {data.daño}";

        if (!string.IsNullOrEmpty(data.efectoExtra))
        {
            efectoExtra.text = $"Efecto: {data.efectoExtra}";
            efectoExtra.gameObject.SetActive(true);
        }
        else
        {
            efectoExtra.text = "";
            efectoExtra.gameObject.SetActive(false);
        }
    }

    private void LimpiarPagina(TextMeshProUGUI titulo, Image imagen, TextMeshProUGUI descripcion, TextMeshProUGUI daño, TextMeshProUGUI efectoExtra)
    {
        titulo.text = "";
        imagen.sprite = null;
        imagen.enabled = false;
        descripcion.text = "";
        daño.text = "";
        efectoExtra.text = "";
        efectoExtra.gameObject.SetActive(false);
    }
}
