using UnityEngine;

public class NivelManagerDesdeAmuletos : MonoBehaviour
{
    private int nivelJugador;

    private void Start()
    {
        // Cargar nivel guardado, si no existe, empezar en 1
        nivelJugador = PlayerPrefs.GetInt("NivelJugador", 1);
        Debug.Log($"Nivel cargado al iniciar MejorasScene: {nivelJugador}");
    }

    // Llamar este método cuando el jugador elige mejorar o subir nivel
    public void SubirNivel()
    {
        nivelJugador++;
        PlayerPrefs.SetInt("NivelJugador", nivelJugador);
        PlayerPrefs.Save();

        Debug.Log($"Nivel actualizado y guardado: {nivelJugador}");
    }

    // Opcional: para resetear nivel, útil para debugging o reiniciar
    public void ResetNivel()
    {
        nivelJugador = 1;
        PlayerPrefs.SetInt("NivelJugador", nivelJugador);
        PlayerPrefs.Save();

        Debug.Log("Nivel reseteado a 1");
    }

    // Método para obtener el nivel actual (por si querés mostrar en UI)
    public int ObtenerNivel()
    {
        return nivelJugador;
    }
}
