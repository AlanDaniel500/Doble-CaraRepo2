using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayDobleCara : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Botón Play presionado");
        SceneManager.LoadScene("GAME");
    }
}
