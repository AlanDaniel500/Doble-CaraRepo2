using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DealCard : MonoBehaviour
{
    public GameObject[] dealCard;
    public int cardGenerate;

    public void DealMyNewCard()
    {
        cardGenerate = Random.Range(1, 3);
        dealCard[0].SetActive(true);
    }
}
