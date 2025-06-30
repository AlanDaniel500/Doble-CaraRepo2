using UnityEngine;
using UnityEngine.SceneManagement;

public class Gano : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;  // Cambiado aquí

    private void Update()
    {
        if (enemyController != null && enemyController.VidaActual <= 0)
        {
            PlayerPrefs.SetInt("GameResult", 1); // Guardamos victoria
            PlayerPrefs.Save();
            SceneManager.LoadScene("EndScreen");
        }
    }
}
