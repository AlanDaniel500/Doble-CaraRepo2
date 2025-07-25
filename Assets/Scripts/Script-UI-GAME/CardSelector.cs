using UnityEngine;
using DG.Tweening;

public class CardSelector : MonoBehaviour
{
    private bool isSelected = false;
    private Vector3 originalPosition;
    private Vector3 originalScale;
    public float liftAmount = 0.3f;

    // Config animación
    public float hoverScaleMultiplier = 1.1f;
    public float animationDuration = 0.2f;

    private static int cartasLevantadas = 0;

    [SerializeField] private int maxCartasPermitidas = 8;
    private static int maxCartasLevantadas;

    private void Start()
    {
        originalPosition = transform.position;
        originalScale = transform.localScale;

        if (maxCartasLevantadas == 0)
        {
            maxCartasLevantadas = maxCartasPermitidas;
        }
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            if (cartasLevantadas < maxCartasLevantadas)
            {
                transform.position = originalPosition + new Vector3(0, liftAmount, 0);
                isSelected = true;
                cartasLevantadas++;
            }
            else
            {
                Debug.Log($"Ya hay {maxCartasLevantadas} cartas levantadas.");
            }
        }
        else
        {
            transform.position = originalPosition;
            isSelected = false;
            cartasLevantadas--;
        }
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            transform.DOScale(originalScale * hoverScaleMultiplier, animationDuration).SetEase(Ease.OutBack);
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            transform.DOScale(originalScale, animationDuration).SetEase(Ease.OutBack);
        }
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public static void ReiniciarContador()
    {
        cartasLevantadas = 0;
    }
}
