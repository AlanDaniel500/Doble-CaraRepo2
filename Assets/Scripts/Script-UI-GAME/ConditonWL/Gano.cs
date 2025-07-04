using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Gano : MonoBehaviour
{
    private EnemyController enemyController;
    private bool victoriaMostrada = false;

    private void Start()
    {
        StartCoroutine(EsperarVictoria());
    }

    private IEnumerator EsperarVictoria()
    {
        float timeout = 5f;
        float tiempo = 0f;

        while (enemyController == null && tiempo < timeout)
        {
            enemyController = FindFirstObjectByType<EnemyController>();
            tiempo += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        if (enemyController == null)
        {
            Debug.LogWarning("[Gano] No se encontró EnemyController luego de esperar.");
            yield break;
        }

        // Esperar a que el enemigo esté activo
        while (!enemyController.EstaActivo() && tiempo < timeout)
        {
            tiempo += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        if (!enemyController.EstaActivo())
        {
            Debug.LogWarning("[Gano] EnemyController no se activó correctamente.");
            yield break;
        }

        // Ahora sí, esperar a que su vida llegue a 0
        while (enemyController.VidaActual > 0)
        {
            yield return null;
        }

        if (!victoriaMostrada)
        {
            victoriaMostrada = true;
            PlayerPrefs.SetInt("GameResult", 1);
            PlayerPrefs.Save();
            Debug.Log("[Gano] ¡Victoria detectada! Cargando EndScreen...");
            SceneManager.LoadScene("EndScreen");
        }
    }
}
