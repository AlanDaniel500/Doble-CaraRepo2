using UnityEngine;

public class PlayDobleCara : MonoBehaviour
{
    public GameObject tutorialCanvas; // Asignar el canvas del tutorial en el inspector

    public void PlayGame()
    {
        Debug.Log("Botón Play presionado");
        tutorialCanvas.SetActive(true);
    }
}
