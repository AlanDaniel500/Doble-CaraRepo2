using UnityEngine;
using UnityEngine.UI; // <- importante para usar Image
using CardSystem;

public class CardDisplay : MonoBehaviour
{
    public CardData CardData;
    public Image cardImageUI; // <- en vez de SpriteRenderer

    public void LoadCardData()
    {
        if (CardData != null && cardImageUI != null)
        {
            cardImageUI.sprite = CardData.cardImage;
        }
    }
}
