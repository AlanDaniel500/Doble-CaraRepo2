using UnityEngine;
using UnityEngine.SceneManagement;

public class Gano : MonoBehaviour
{
    [SerializeField] private RecibeDanoEnemigo enemigo;

    private void Update()
    {
        if (enemigo != null && enemigo.VidaActual <= 0)
        {
            PlayerPrefs.SetInt("GameResult", 1); // Guardamos victoria
            PlayerPrefs.Save();
            SceneManager.LoadScene("EndScreen");
        }
    }
}
