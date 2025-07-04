using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Niveles")]
    [SerializeField] private NivelData[] niveles;

    private int nivelActual = 0;
    private EnemyController enemyActual = null;

    private const string NivelKey = "NivelJugador";
    private const string GameResultKey = "GameResult";

    private bool nivelCargando = false;

    [Header("Nombres de escena")]
    [SerializeField] private string escenaMejoras = "MejorasScene";
    [SerializeField] private string escenaCombate = "GAME"; // 🔧 CORREGIDO: nombre real de tu escena

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("[LevelManager] Awake: Instancia creada.");
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Debug.Log("[LevelManager] Start: Cargando nivel guardado...");
        StartCoroutine(IniciarNivelDesdeGuardado());
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[LevelManager] Escena cargada: {scene.name}. Buscando EnemyController...");

        EnemyController enemy = FindFirstObjectByType<EnemyController>();
        if (enemy != null)
        {
            RegistrarEnemy(enemy);
        }
        else
        {
            Debug.LogWarning("[LevelManager] EnemyController no encontrado.");
        }
    }

    public void RegistrarEnemy(EnemyController enemy)
    {
        enemyActual = enemy;
        Debug.Log("[LevelManager] Enemy registrado.");

        if (nivelActual >= 0 && nivelActual < niveles.Length)
        {
            NivelData datos = niveles[nivelActual];
            enemyActual.SetStats(datos.vidaEnemigo, datos.dañoEnemigo, datos.turnosParaAtacar, datos.spriteEnemigo);
            Debug.Log($"[LevelManager] Stats enemigo aplicados para nivel {nivelActual + 1}.");
        }
    }

    private IEnumerator IniciarNivelDesdeGuardado()
    {
        int gameResult = PlayerPrefs.GetInt(GameResultKey, 1);

        if (gameResult == 0)
        {
            Debug.Log("[LevelManager] Reiniciando a nivel 1 por derrota previa.");
            PlayerPrefs.SetInt(NivelKey, 1);
            PlayerPrefs.SetInt(GameResultKey, 1);
            PlayerPrefs.Save();
        }

        nivelActual = PlayerPrefs.GetInt(NivelKey, 1) - 1;
        nivelActual = Mathf.Clamp(nivelActual, 0, niveles.Length - 1);

        Debug.Log($"[LevelManager] Nivel cargado desde guardado: {nivelActual + 1}");

        float tiempo = 0f;
        while (enemyActual == null && tiempo < 3f)
        {
            EnemyController enemy = FindFirstObjectByType<EnemyController>();
            if (enemy != null)
            {
                RegistrarEnemy(enemy);
                break;
            }
            yield return new WaitForSeconds(0.1f);
            tiempo += 0.1f;
        }

        MostrarPantallaNivel();
    }

    public void SubirDeNivel()
    {
        if (nivelCargando)
        {
            Debug.LogWarning("[LevelManager] Nivel ya en carga.");
            return;
        }

        if (nivelActual + 1 < niveles.Length)
        {
            nivelActual++;
            PlayerPrefs.SetInt(NivelKey, nivelActual + 1);
            PlayerPrefs.SetInt(GameResultKey, 1);
            PlayerPrefs.Save();

            Debug.Log($"[LevelManager] Subiendo al nivel {nivelActual + 1} y cargando escena de mejoras...");
            nivelCargando = true;

            SceneManager.LoadScene(escenaMejoras);
        }
        else
        {
            Debug.Log("[LevelManager] Último nivel completado. Fin del juego.");
            // Podés cargar una escena de final aquí
        }
    }

    public void ContinuarCombate()
    {
        Debug.Log("[LevelManager] Continuando al combate...");
        nivelCargando = false;
        SceneManager.LoadScene(escenaCombate); // ✅ Ahora usa "GAME"
    }

    public void MostrarPantallaNivel()
    {
        PantallaNivel pantalla = FindFirstObjectByType<PantallaNivel>();
        if (pantalla != null)
        {
            pantalla.SetNivel(nivelActual + 1);
            pantalla.MostrarNivelConFade();
        }
        else
        {
            Debug.LogWarning("[LevelManager] PantallaNivel no encontrada.");
            ComenzarNivel();
        }
    }

    public void ComenzarNivel()
    {
        Debug.Log("[LevelManager] Nivel iniciado.");
    }

    public int GetNivelActual() => nivelActual + 1;
}
