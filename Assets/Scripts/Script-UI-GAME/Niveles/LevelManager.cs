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
    [SerializeField] private string escenaCombate = "GAME";
    [SerializeField] private string escenaFinal = "EndScreen";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnemyController enemy = FindFirstObjectByType<EnemyController>();
        if (enemy != null)
        {
            RegistrarEnemy(enemy);
        }
    }

    public void RegistrarEnemy(EnemyController enemy)
    {
        enemyActual = enemy;

        if (nivelActual >= 0 && nivelActual < niveles.Length)
        {
            NivelData datos = niveles[nivelActual];

            // Pasamos el jefeID = nivelActual (puede ser cualquier entero que uses para animaciones)
            enemyActual.SetStats(
                datos.vidaEnemigo,
                datos.dañoEnemigo,
                datos.turnosParaAtacar,
                nivelActual, // JefeID para animaciones
                datos.spriteEnemigo
            );
        }
    }

    // El resto del código queda igual (IniciarNivelDesdeGuardado, SubirDeNivel, etc.)
    // ...

    public IEnumerator IniciarNivelDesdeGuardado()
    {
        int gameResult = PlayerPrefs.GetInt(GameResultKey, 1);

        if (gameResult == 0)
        {
            PlayerPrefs.SetInt(NivelKey, 1);
            PlayerPrefs.SetInt(GameResultKey, 1);
            PlayerPrefs.Save();
        }

        nivelActual = PlayerPrefs.GetInt(NivelKey, 1) - 1;
        nivelActual = Mathf.Clamp(nivelActual, 0, niveles.Length - 1);

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
        if (nivelCargando) return;

        if (nivelActual + 1 < niveles.Length)
        {
            nivelActual++;
            PlayerPrefs.SetInt(NivelKey, nivelActual + 1);
            PlayerPrefs.SetInt(GameResultKey, 1);
            PlayerPrefs.Save();

            nivelCargando = true;
            SceneManager.LoadScene(escenaMejoras);
        }
        else
        {
            PlayerPrefs.SetInt(GameResultKey, 1);
            PlayerPrefs.Save();

            nivelCargando = true;
            SceneManager.LoadScene(escenaFinal);
        }
    }

    public void ContinuarCombate()
    {
        nivelCargando = false;
        SceneManager.LoadScene(escenaCombate);
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
            ComenzarNivel();
        }
    }

    public void ComenzarNivel()
    {
        // Aquí va lo que necesites al comenzar el nivel
    }

    public int GetNivelActual() => nivelActual + 1;

    public NivelData GetNivelData()
    {
        if (nivelActual >= 0 && nivelActual < niveles.Length)
            return niveles[nivelActual];
        return null;
    }
}
