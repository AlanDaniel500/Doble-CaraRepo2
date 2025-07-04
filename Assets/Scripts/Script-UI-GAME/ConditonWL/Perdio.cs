using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Perdio : MonoBehaviour
{
    [SerializeField] private PlayerHealthUI playerHealthUI;
    private bool derrotaMostrada = false;

    private void Start()
    {
        StartCoroutine(EsperarDerrota());
    }

    private IEnumerator EsperarDerrota()
    {
        float timeout = 5f;
        float tiempo = 0f;

        // Esperar a que PlayerHealthUI aparezca en escena
        while (playerHealthUI == null && tiempo < timeout)
        {
            playerHealthUI = FindFirstObjectByType<PlayerHealthUI>();
            tiempo += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        if (playerHealthUI == null)
        {
            Debug.LogWarning("[Perdio] No se encontró PlayerHealthUI luego de esperar.");
            yield break;
        }

        // Esperar a que su vida llegue a 0
        while (playerHealthUI.CurrentHealth > 0)
        {
            yield return null;
        }

        if (!derrotaMostrada)
        {
            derrotaMostrada = true;
            PlayerPrefs.SetInt("GameResult", 0);
            PlayerPrefs.Save();

            Debug.Log("[Perdio] Jugador murió. Cargando EndScreen (derrota).");
            SceneManager.LoadScene("EndScreen");
        }
    }
}
