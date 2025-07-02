using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    public int maxhealth = 100;
    public int currenthealth;




    private void Start()
    {
        currenthealth = maxhealth;
    }
}
