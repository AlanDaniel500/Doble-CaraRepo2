using UnityEngine;

public class EnemyCard : MonoBehaviour
{
   public int life = 1000;
    public int damage = 100;
    public int turnattack = 2;
    private int counterturn = 0;


    public PlayerHealthUI player;






    public void takedamage( int amount)
    {
        life -= amount;
        if(life <= 0 )
        {
            death();
        }
    }

    public void turn()
    {
        counterturn++;
        if(counterturn >= turnattack)
        {
            Attack();
            counterturn = 0;

        }
    }

    private void Attack()
    {
        
        Debug.Log("el enemigo ataca con" + damage + "de daño.");
        if(player != null )
        {
            player.TakeDamage(damage);

        }

    }

    private void death()
    {
        Debug.Log("el enemigo ha muerto.");
        Destroy(gameObject);

    }
}
