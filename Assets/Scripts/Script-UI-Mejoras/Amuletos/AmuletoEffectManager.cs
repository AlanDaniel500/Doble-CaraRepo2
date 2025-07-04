using UnityEngine;
using System.Collections;

public class AmuletoEffectManager : MonoBehaviour
{
    public static AmuletoEffectManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 🟢 No se destruye al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Se destruye si ya hay una instancia
        }
    }

    public void AplicarEfectos(AmuletoListaEfectos amuleto)
    {
        if (amuleto == null)
        {
            Debug.LogWarning("No se recibió un amuleto para aplicar efectos.");
            return;
        }

        StartCoroutine(EsperarYAplicar(amuleto));
    }

    private IEnumerator EsperarYAplicar(AmuletoListaEfectos amuleto)
    {
        PlayerHealthUI playerHealth = null;
        float timeout = 5f;
        float tiempo = 0f;

        // Espera hasta que PlayerHealthUI esté disponible o se acabe el tiempo
        while (playerHealth == null && tiempo < timeout)
        {
            playerHealth = Object.FindFirstObjectByType<PlayerHealthUI>();
            tiempo += Time.deltaTime;
            yield return null;
        }

        if (playerHealth == null)
        {
            Debug.LogWarning("No se encontró el componente PlayerHealthUI en la escena después de esperar.");
            yield break;
        }

        Debug.Log("Aplicando efectos del amuleto: " + amuleto.nombre);

        if (amuleto.aumentaVida)
        {
            playerHealth.AumentarVidaMaxima(amuleto.cantidadAumentoVida);
            playerHealth.CurarAlMaximo();
            Debug.Log($"Efecto: Aumenta vida máxima en {amuleto.cantidadAumentoVida} puntos.");
        }

        if (amuleto.aumentaDaño)
        {
            // Lógica para aumentar daño
            Debug.Log("Efecto: Aumenta daño.");
        }

        if (amuleto.reduceTurnosEnemigo)
        {
            // Lógica para reducir turnos del enemigo
            Debug.Log("Efecto: Reduce turnos del enemigo.");
        }

        if (amuleto.curaAlInicio)
        {
            // Lógica para curar al inicio del combate
            Debug.Log("Efecto: Cura al inicio.");
        }

        if (amuleto.duplicarRecompensas)
        {
            // Lógica para duplicar recompensas
            Debug.Log("Efecto: Duplicar recompensas.");
        }
    }
}
