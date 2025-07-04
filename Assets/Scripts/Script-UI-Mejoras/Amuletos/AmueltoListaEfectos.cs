using UnityEngine;

[CreateAssetMenu(fileName = "NuevoAmuleto", menuName = "Amuletos/Amuleto Lista Efectos")]
public class AmuletoListaEfectos : ScriptableObject
{
    [Header("Información del Amuleto")]
    public string nombre;
    public Sprite imagen;
    [TextArea(2, 4)]
    public string descripcion;

    [Header("Efectos (para programar luego)")]
    public bool aumentaVida;
    public int cantidadAumentoVida; //


    public bool aumentaDaño;
    public bool reduceTurnosEnemigo;
    public bool curaAlInicio;
    public bool duplicarRecompensas;
}
