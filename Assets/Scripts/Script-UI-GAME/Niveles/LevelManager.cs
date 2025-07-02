using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Niveles")]
    [SerializeField] private NivelData[] niveles;

    [Header("Enemigo")]
    [SerializeField] private EnemyController enemy;

    [Header("UI de Nivel")]
    [SerializeField] private PantallaNivel pantallaNivel;

    private int nivelActual = 0;

    private const string NivelKey = "NivelJugador";
    private const string GameResultKey = "GameResult";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Que este objeto persista si querés:
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int gameResult = PlayerPrefs.GetInt(GameResultKey, 1);

        if (gameResult == 0)
        {
            Debug.Log("Jugador perdió en la sesión anterior, reseteando nivel a 1.");
            PlayerPrefs.SetInt(NivelKey, 1);
            PlayerPrefs.SetInt(GameResultKey, 1);
            PlayerPrefs.Save();
        }

        nivelActual = PlayerPrefs.GetInt(NivelKey, 1) - 1;
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

        if (AudioManager.Instance != null && datos.musicaDelNivel != null)
        {
        AudioManager.Instance.PlayMusic(datos.musicaDelNivel);
        }

        Debug.Log("Nivel cargado: " + (nivelActual + 1));

        // Mostrar la pantalla de nivel con fade
        MostrarPantallaNivel();




    }

    public void SubirDeNivel()
    {
        if (nivelActual + 1 < niveles.Length)
        {
            nivelActual++;
            PlayerPrefs.SetInt(NivelKey, nivelActual + 1);
            PlayerPrefs.SetInt(GameResultKey, 1);
            PlayerPrefs.Save();

            CargarNivel(nivelActual);
        }
        else
        {
            Debug.Log("Último nivel alcanzado. Fin del juego.");
        }
    }

    public int GetNivelActual() => nivelActual + 1;

    public void MostrarPantallaNivel()
    {
        if (pantallaNivel != null)
        {
            pantallaNivel.SetNivel(nivelActual + 1);
            pantallaNivel.MostrarNivelConFade(); // Esto ejecuta el fade y luego llama a ComenzarNivel
        }
        else
        {
            Debug.LogWarning("PantallaNivel no asignada.");
            // En caso de no tener pantallaNivel, arrancamos directamente el nivel
            ComenzarNivel();
        }
    }

    public void ComenzarNivel()
    {
        // Este método se llama desde PantallaNivel cuando termina el fade
        Debug.Log("Nivel iniciado.");
        // Acá iniciá la lógica para que empiece el nivel, spawn enemigos, turnos, etc.
    }
}
