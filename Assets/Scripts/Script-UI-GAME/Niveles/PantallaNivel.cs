using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PantallaNivel : MonoBehaviour
{
    [SerializeField] private Image panelNegro;
    [SerializeField] private TMP_Text textoNivel;
    [SerializeField] private float duracionMostrar = 2f;
    [SerializeField] private float duracionFade = 1f;

    private Coroutine mostrarNivelCoroutine;

    private void Start()
    {
        // Si querés que arranque automático al inicio, descomenta:
        //panelNegro.gameObject.SetActive(true);
        //mostrarNivelCoroutine = StartCoroutine(MostrarNivel());
    }

    public void SetNivel(int nivel)
    {
        textoNivel.text = $"Nivel {nivel}";
    }

    // Método público para activar el panel y mostrar nivel con fade
    public void MostrarNivelConFade()
    {
        if (mostrarNivelCoroutine != null)
        {
            StopCoroutine(mostrarNivelCoroutine);
        }
        panelNegro.gameObject.SetActive(true);
        SetAlpha(1f);
        mostrarNivelCoroutine = StartCoroutine(MostrarNivel());
    }

    private IEnumerator MostrarNivel()
    {
        // Mostrar texto nivel un tiempo fijo
        yield return new WaitForSeconds(duracionMostrar);

        // Fade out gradual
        float tiempo = 0f;
        while (tiempo < duracionFade)
        {
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, tiempo / duracionFade);
            SetAlpha(alpha);
            yield return null;
        }

        // Desactivar panel negro y texto
        panelNegro.gameObject.SetActive(false);

        // Avisar que terminó (por ejemplo, llamar un método del LevelManager)
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.ComenzarNivel();
        }
    }

    private void SetAlpha(float alpha)
    {
        Color c = panelNegro.color;
        c.a = alpha;
        panelNegro.color = c;

        Color t = textoNivel.color;
        t.a = alpha;
        textoNivel.color = t;
    }
}
