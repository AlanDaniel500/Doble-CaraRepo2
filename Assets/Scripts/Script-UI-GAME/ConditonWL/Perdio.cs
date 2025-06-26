using UnityEngine;
using UnityEngine.SceneManagement;

public class Perdio : MonoBehaviour
{
    [SerializeField] private PlayerHealthUI playerHealthUI;

    private void Update()
    {
        if (playerHealthUI != null && playerHealthUI.CurrentHealth <= 0)
        {
            PlayerPrefs.SetInt("GameResult", 0); // Guardamos derrota
            PlayerPrefs.Save();
            SceneManager.LoadScene("EndScreen");
        }
    }
}
