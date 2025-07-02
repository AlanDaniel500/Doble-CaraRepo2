using UnityEngine;

public class AmuletEffectManager : MonoBehaviour
{
    public static AmuletEffectManager Instance;

    private int bonusMaxHealth = 0;
    private float damageMultiplier = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetBonusMaxHealth(int value) => bonusMaxHealth = value;
    public int GetBonusMaxHealth() => bonusMaxHealth;

    public void SetDamageMultiplier(float value) => damageMultiplier = value;
    public float GetDamageMultiplier() => damageMultiplier;

    public void ResetEffects()
    {
        bonusMaxHealth = 0;
        damageMultiplier = 1f;
    }
}
