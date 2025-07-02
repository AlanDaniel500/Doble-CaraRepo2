using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AmuletSelectorUI : MonoBehaviour
{
    [System.Serializable]
    public class AmuletUI
    {
        public Button button;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
    }

    public List<AmuletUI> amuletButtons;
    public string nextScene = "GAME";

    private List<AmuletEffect> allAmulets = new List<AmuletEffect>();
    private List<AmuletEffect> selectedAmulets = new List<AmuletEffect>();

    private void Start()
    {
        LoadAllAmulets();
        SelectRandomAmulets();

        for (int i = 0; i < amuletButtons.Count; i++)
        {
            int index = i;
            var ui = amuletButtons[i];
            var effect = selectedAmulets[i];

            ui.nameText.text = effect.name;
            ui.descriptionText.text = effect.description;

            ui.button.onClick.RemoveAllListeners();
            ui.button.onClick.AddListener(() =>
            {
                ApplyAmuletEffect(effect);
                SceneManager.LoadScene(nextScene);
            });
        }
    }

    void LoadAllAmulets()
    {
        allAmulets = new List<AmuletEffect>()
        {
            new AmuletEffect("Vitality", "Gain +200 max health", () =>
            {
                PlayerPrefs.SetInt("Player_MaxHealth_Bonus", 200);
            }),

            new AmuletEffect("Bloodthirst", "Double card damage", () =>
            {
                PlayerPrefs.SetFloat("Player_DamageMultiplier", 2f);
            }),

            new AmuletEffect("Fortress", "Block next 3 hits", () =>
            {
                PlayerPrefs.SetInt("Player_Shields", 3);
            })
        };
    }

    void SelectRandomAmulets()
    {
        selectedAmulets.Clear();
        List<AmuletEffect> temp = new List<AmuletEffect>(allAmulets);

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, temp.Count);
            selectedAmulets.Add(temp[rand]);
            temp.RemoveAt(rand);
        }
    }

    void ApplyAmuletEffect(AmuletEffect effect)
    {
        effect.applyEffect?.Invoke();
    }
}
