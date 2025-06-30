using UnityEngine;

public class TesteoNivelManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetInt("NivelJugador", 1);
            PlayerPrefs.Save();
            Debug.Log("Nivel reseteado a 1 desde TesteoNivelManager con tecla P");
        }
    }
}
