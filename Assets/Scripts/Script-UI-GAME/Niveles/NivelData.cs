using UnityEngine;

[CreateAssetMenu(fileName = "NuevoNivel", menuName = "Datos/Nivel")]
public class NivelData : ScriptableObject
{
    public int nivelID;
    public int vidaEnemigo;
    public int dañoEnemigo;
    public int turnosParaAtacar;

    public Sprite spriteEnemigo;
    public AudioClip musicaDelNivel;

    // NUEVO: Referencia al controlador o nombre de animación idle
    public RuntimeAnimatorController animadorEnemigo; // OPCIÓN A
    public string nombreAnimacionIdle;                // OPCIÓN B (por si no querés cambiar todo el controller)
}
