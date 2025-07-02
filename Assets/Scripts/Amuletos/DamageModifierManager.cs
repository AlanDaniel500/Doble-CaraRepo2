using UnityEngine;

public class DamageModifierManager : MonoBehaviour
{
    public static DamageModifierManager Instance;
    private float damageMultiplier = 1f;
    private int shields = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetDamageMultiplier(float value) => damageMultiplier = value;
    public float GetDamageMultiplier() => damageMultiplier;

    public void SetShields(int count) => shields = count;
    public bool TryBlockDamage()
    {
        if (shields > 0)
        {
            shields--;
            return true;
        }
        return false;
    }
}

