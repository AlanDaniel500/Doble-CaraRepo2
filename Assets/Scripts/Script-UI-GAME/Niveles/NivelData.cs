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

    // Ya no se necesita animador individual, es un solo animator controller
    // public RuntimeAnimatorController animadorEnemigo;
}
