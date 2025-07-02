using UnityEngine;

public abstract class AmuletoBase : ScriptableObject
{
    public string Name;
    public Sprite Icono;


    public abstract void AplicarEfecto(StatsPlayer player);


    public virtual void RemoverObjeto(StatsPlayer player)
    {

    }
}
