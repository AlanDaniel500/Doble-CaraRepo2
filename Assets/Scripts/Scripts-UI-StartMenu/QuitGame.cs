using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void QuitApp()
    {
        Debug.Log("Botón Quit presionado");
        Application.Quit();
    }
}
