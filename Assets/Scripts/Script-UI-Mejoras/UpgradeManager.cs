using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    [Header("Botón de confirmación")]
    [SerializeField] private Button confirmButton;

    [Header("Botones de amuleto")]
    [SerializeField] private Button botonOpcion1;
    [SerializeField] private Button botonOpcion2;
    [SerializeField] private Button botonOpcion3;

    // Colores para el efecto visual
    [Header("Colores de selección")]
    [SerializeField] private Color colorSeleccionado = Color.white;
    [SerializeField] private Color colorNoSeleccionado = Color.gray;

    private Button[] charmButtons;
    private int selectedCharmIndex = -1;

    void Start()
    {
        // Inicializar botones
        charmButtons = new Button[] { botonOpcion1, botonOpcion2, botonOpcion3 };

        confirmButton.gameObject.SetActive(false);
        confirmButton.onClick.AddListener(OnConfirm);

        for (int i = 0; i < charmButtons.Length; i++)
        {
            int index = i;
            charmButtons[i].onClick.AddListener(() => OnCharmSelected(index));
        }

        ResetButtonColors();
    }

    void OnCharmSelected(int index)
    {
        selectedCharmIndex = index;
        confirmButton.gameObject.SetActive(true);
        Debug.Log("Se seleccionó un botón: " + index);

        for (int i = 0; i < charmButtons.Length; i++)
        {
            Image img = charmButtons[i].GetComponent<Image>();
            if (img != null)
                img.color = (i == index) ? colorSeleccionado : colorNoSeleccionado;
        }
    }

    void ResetButtonColors()
    {
        foreach (Button btn in charmButtons)
        {
            Image img = btn.GetComponent<Image>();
            if (img != null)
                img.color = colorNoSeleccionado;
        }
    }

    void OnConfirm()
    {
        Debug.Log("Confirmado: charm " + selectedCharmIndex);
        SceneManager.LoadScene("GAME");
    }
}
