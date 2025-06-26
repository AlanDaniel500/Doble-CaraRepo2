using UnityEngine;

public class CardDraggable : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 originalPosition;
    private bool isDropped = false;

    private void OnMouseDown()
    {
        if (isDropped) return;

        offset = transform.position - GetMouseWorldPosition();
        originalPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        if (isDropped) return;

        transform.position = GetMouseWorldPosition() + offset;
    }

    private void OnMouseUp()
    {

        if (isDropped) return;

        // Verificar si la carta fue soltada sobre una zona de drop
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<ICardDropArea>(out var dropArea))
            {
                dropArea.OnCardDrop(gameObject);
                isDropped = true;
                return;
            }
        }

        transform.position = originalPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        return pos;
    }
}
