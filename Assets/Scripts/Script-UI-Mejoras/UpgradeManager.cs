using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    [Header("Amuletos disponibles")]
    [SerializeField] private List<AmuletoListaEfectos> amuletosDisponibles;

    [Header("UI")]
    [SerializeField] private UIAmuletoHandler uiHandler;

    private void Start()
    {
        MostrarOpciones();
        uiHandler.confirmarBoton.onClick.AddListener(ConfirmarSeleccion);
    }

    void MostrarOpciones()
    {
        List<AmuletoListaEfectos> seleccionados = new List<AmuletoListaEfectos>();
        List<int> indicesYaElegidos = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, amuletosDisponibles.Count);
            } while (indicesYaElegidos.Contains(index));

            indicesYaElegidos.Add(index);
            seleccionados.Add(amuletosDisponibles[index]);
        }

        uiHandler.MostrarAmuletos(seleccionados);
    }

    void ConfirmarSeleccion()
    {
        int nivelActual = PlayerPrefs.GetInt("NivelJugador", 1);
        PlayerPrefs.SetInt("NivelJugador", nivelActual + 1);

        var amuleto = uiHandler.GetAmuletoSeleccionado();
        if (amuleto != null)
        {
            PlayerPrefs.SetString("AmuletoNombre", amuleto.nombre);
            // Guardar efectos u otras variables si querés
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene("GAME");
    }
}
