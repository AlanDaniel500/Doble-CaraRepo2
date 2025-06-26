using UnityEngine;

public class OptionsPanelManager : MonoBehaviour
{
    public GameObject optionsPanel;

    public void ShowOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void HideOptions()
    {
        optionsPanel.SetActive(false);
    }
}
