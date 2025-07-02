using System;
using UnityEngine;



[CreateAssetMenu(menuName ="Amuleto/VidaMaxima")]
public class VidaMax : AmuletoBase
{
    public int MaxLife = 100;


    public override void AplicarEfecto(StatsPlayer player)
    {
        player.maxhealth += MaxLife;
        player.currenthealth += MaxLife;

    }


    public override void RemoverObjeto(StatsPlayer player)
    {
        player.maxhealth -= MaxLife;
        player.currenthealth = Mathf.Min(player.currenthealth,player.maxhealth);
    }
}
