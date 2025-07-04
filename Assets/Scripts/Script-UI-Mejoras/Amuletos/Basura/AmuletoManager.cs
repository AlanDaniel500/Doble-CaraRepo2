using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmuletManager : MonoBehaviour
{
    /*public List<AmuletBase> equippedAmulets = new List<AmuletBase>();
    private PlayerStats stats;

    public Transform availablePanel;
    public Transform equippedPanel;
    public GameObject equippedButtonPrefab;
    public List<AmuletBase> allAmulets;

    private AmuletManager amuletManager;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();
        amuletManager = FindFirstObjectByType<AmuletManager>();
        ShowEquippedAmulets();
    }

    public void EquipAmulet(AmuletBase amulet)
    {
        equippedAmulets.Add(amulet);
        amulet.ApplyEffect(stats);
        ShowEquippedAmulets();
    }

    public void RemoveAmulet(AmuletBase amulet)
    {
        if (equippedAmulets.Remove(amulet))
        {
            amulet.RemoveEffect(stats);
            ShowEquippedAmulets();
        }
    }

    private void ShowEquippedAmulets()
    {
        foreach (Transform child in equippedPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var amulet in equippedAmulets)
        {
            GameObject buttonGO = Instantiate(equippedButtonPrefab, equippedPanel);
            buttonGO.GetComponentInChildren<Text>().text = amulet.amuletName;
            buttonGO.GetComponent<Button>().onClick.AddListener(() =>
            {
                RemoveAmulet(amulet);
            });
        }
    }*/
}
