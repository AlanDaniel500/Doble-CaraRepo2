using DG.Tweening;
using UnityEngine;

public class FlipCard : MonoBehaviour
{
    private bool flipped = false;

    public void Flip() //  importante que sea public
    {
        flipped = !flipped;

        // Gira la carta en eje Y (180 = dorso, 0 = frente)
        transform.DORotate(new Vector3(0, flipped ? 0f : 180f, 0), 0.25f);
    }
}