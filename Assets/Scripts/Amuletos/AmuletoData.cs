using UnityEngine;

[CreateAssetMenu(fileName = "NuevoAmuleto", menuName = "Amuletos/Amuleto")]
public class AmuletoData : ScriptableObject
{
    [Header("Información del Amuleto")]
    public string nombre;
    public Sprite icono;
    [TextArea(2, 4)]
    public string descripcion;

    [Header("Efectos del Amuleto")]
    public bool aumentaVida;
    public bool aumentaDaño;
    public bool reduceTurnosEnemigo;
    public bool curaAlInicio;
    public bool duplicarRecompensas;
    // Agregá más flags según lo que vayas a implementar
}
