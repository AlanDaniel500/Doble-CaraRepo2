using UnityEngine;

public class TesteoSubirNivel : MonoBehaviour
{
    [SerializeField] private PantallaNivel pantallaNivel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pantallaNivel != null)
            {
                Debug.Log("TesteoSubirNivel: mostrando pantalla con fade.");
                pantallaNivel.SetNivel(1);  // probamos con nivel 1
                pantallaNivel.MostrarNivelConFade();
            }
            else
            {
                Debug.LogWarning("PantallaNivel no asignada en TesteoSubirNivel.");
            }
        }
    }
}
