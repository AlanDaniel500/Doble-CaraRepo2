using UnityEngine;

[CreateAssetMenu(fileName = "NuevoNivel", menuName = "Datos/Nivel")]
public class NivelData : ScriptableObject
{
    public int nivelID;
    public int vidaEnemigo;
    public int dañoEnemigo;
    public int turnosParaAtacar;
    public Sprite spriteEnemigo; // <-- ESTE CAMPO DEBE ESTAR DEFINIDO
    public AudioClip musicaDelNivel;
    // Podés agregar más cosas después como música, fondo, nombre del jefe, etc.
}
