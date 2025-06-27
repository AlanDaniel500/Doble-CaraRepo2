using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Valores de Vida")]
    [SerializeField] private int maxHealth = 1000;
    [SerializeField] private int currentHealth = 1000;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText;

    // Propiedad pública para que otros scripts puedan leer la vida actual
    public int CurrentHealth => currentHealth;

    void Start()
    {
        UpdateHealthUI();
    }

    void Update()
    {
        // Testeo con teclas
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(100);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Heal(100);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = $"{currentHealth} / {maxHealth}";
    }
}
