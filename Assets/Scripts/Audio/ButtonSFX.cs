using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    private AudioSource sfxSource;

    private void Start()
    {
        // Usar el AudioManager si lo tenés, o crear un AudioSource local
        if (AudioManager.Instance != null)
        {
            sfxSource = AudioManager.Instance.GetSFXSource(); // Asegurate que esto exista
        }
        else
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null && sfxSource != null)
            sfxSource.PlayOneShot(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null && sfxSource != null)
            sfxSource.PlayOneShot(clickSound);
    }
}
