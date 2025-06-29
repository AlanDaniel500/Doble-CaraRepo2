using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Niveles")]
    [SerializeField] private NivelData[] niveles;

    [Header("Enemigo")]
    [SerializeField] private EnemyController enemy;

    private int nivelActual = 0;

    private const string NivelKey = "NivelJugador";
    private const string GameResultKey = "GameResult";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Revisar resultado del juego anterior (1 = ganó o no hay dato, 0 = perdió)
        int gameResult = PlayerPrefs.GetInt(GameResultKey, 1);

        if (gameResult == 0)
        {
            Debug.Log("Jugador perdió en la sesión anterior, reseteando nivel a 1.");
            PlayerPrefs.SetInt(NivelKey, 1);
            PlayerPrefs.SetInt(GameResultKey, 1); // Reseteamos resultado para la próxima vez
            PlayerPrefs.Save();
        }

        // Leer nivel desde PlayerPrefs (por defecto es 1, por eso restamos 1)
        nivelActual = PlayerPrefs.GetInt(NivelKey, 1) - 1;

        // Asegurarse que esté dentro de rango
        nivelActual = Mathf.Clamp(nivelActual, 0, niveles.Length - 1);

        CargarNivel(nivelActual);
    }

    public void CargarNivel(int id)
    {
        if (id < 0 || id >= niveles.Length)
        {
            Debug.LogWarning("Nivel inválido.");
            return;
        }

        nivelActual = id;
        NivelData datos = niveles[nivelActual];

        if (enemy != null)
        {
            enemy.SetStats(
                datos.vidaEnemigo,
                datos.dañoEnemigo,
                datos.turnosParaAtacar,
                datos.spriteEnemigo
            );
        }
        else
        {
            Debug.LogError("EnemyController no asignado en LevelManager.");
        }

        Debug.Log("Nivel cargado: " + (nivelActual + 1));
    }

    public void SubirDeNivel()
    {
        if (nivelActual + 1 < niveles.Length)
        {
            nivelActual++;

            // Guardar el nuevo nivel (sumamos 1 para mostrar al jugador)
            PlayerPrefs.SetInt(NivelKey, nivelActual + 1);
            PlayerPrefs.SetInt(GameResultKey, 1); // Marcamos que está en juego normal (ganó o siguió)
            PlayerPrefs.Save();

            CargarNivel(nivelActual);
        }
        else
        {
            Debug.Log("Último nivel alcanzado. Fin del juego.");
        }
    }

    public int GetNivelActual() => nivelActual + 1; // Para mostrar al jugador
}
