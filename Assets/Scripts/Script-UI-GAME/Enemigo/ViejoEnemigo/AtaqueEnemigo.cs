using UnityEngine;
using TMPro;

public class AtaqueEnemigo : MonoBehaviour
{
    [Header("Daño del enemigo")]
    [SerializeField] private int damage = 100;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI damageText;

    private PlayerHealthUI playerHealthUI;

    private void Start()
    {
        playerHealthUI = FindFirstObjectByType<PlayerHealthUI>();
        UpdateDamageUI();
    }

    public void EjecutarAtaque()
    {
        Debug.Log("¡El enemigo ataca!");

        if (playerHealthUI != null)
        {
            playerHealthUI.TakeDamage(damage);
        }

        if (CameraShake.Instance != null)
        {
            StartCoroutine(CameraShake.Instance.Shake(0.2f, 0.15f));
        }

        UpdateDamageUI();
    }


    private void UpdateDamageUI()
    {
        if (damageText != null)
            damageText.text = damage.ToString(); // Muestra el daño sin el signo "-"
    }

    public void SetDamage(int nuevoDaño)
    {
        damage = nuevoDaño;
        UpdateDamageUI();
    }
}
