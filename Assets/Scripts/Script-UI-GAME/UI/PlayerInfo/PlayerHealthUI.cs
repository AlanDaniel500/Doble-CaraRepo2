using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Valores de Vida")]
    [SerializeField] private int maxHealth = 1000;
    [SerializeField] private int currentHealth = 1000;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBarImage;

    // Referencia al enemigo
    private EnemyController enemyController;

    // Propiedad pública para acceder a la vida actual del jugador
    public int CurrentHealth => currentHealth;

    void Start()
    {
        UpdateHealthUI();

        // Buscar al primer EnemyController en escena
        enemyController = FindFirstObjectByType<EnemyController>();

        if (enemyController == null)
        {
            Debug.LogWarning("EnemyController no encontrado en la escena.");
        }
    }

    void Update()
    {
        // Testeo: daño al jugador
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(100);
        }
        // Testeo: curación del jugador
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Heal(100);
        }
        // Testeo: daño al enemigo
        else if (Input.GetKeyDown(KeyCode.I))
        {
            if (enemyController != null)
            {
                enemyController.AplicarDanoDesdeCombo(100);
                Debug.Log("Daño aplicado al enemigo con la tecla I.");
            }
            else
            {
                Debug.LogWarning("No se puede dañar al enemigo. EnemyController no asignado.");
            }
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

        if (healthBarImage != null)
            healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }
}
