using UnityEngine;

[CreateAssetMenu(fileName = "NuevaPaginaLibro", menuName = "Libro/Info de Página", order = 0)]
public class InfoCardUIData : ScriptableObject
{
    [Header("Texto")]
    public string titulo;             // Ahora representa el nombre del combo
    [TextArea] public string descripcion;
    
    [Header("Visual")]
    public Sprite imagen;            // Imagen de toda la carta (no partes separadas)

    [Header("Estadísticas")]
    public int daño;

    [Header("Efecto especial (opcional)")]
    [TextArea] public string efectoExtra;  // Si no tiene efecto, se deja vacío
}
