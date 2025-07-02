using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmuletoManager : MonoBehaviour
{
   public List<AmuletoBase> AmuletosEquipados = new List<AmuletoBase>();

    private StatsPlayer stats;


    public Transform panelDisponible;
    public Transform panelEquipados;
    public GameObject panelEquipadosPrefab;
    public List<AmuletoBase> todoslosAmuletos;

    private AmuletoManager amuletoManager;


    private void Start()
    {
       stats=GetComponent<StatsPlayer>();
        amuletoManager = FindObjectOfType<AmuletoManager>();
        MostrarAmuletosEquipados();
        
        
    }


    public void EquiparAmuleto(AmuletoBase amuleto)
    {
        AmuletosEquipados.Add(amuleto);
        amuleto.AplicarEfecto(stats);
    }

    public void QuitarAmuleto(AmuletoBase amuleto)
    {
        if (AmuletosEquipados.Remove(amuleto))
        {
            amuleto.RemoverObjeto(stats);
        }
    }

    void MostrarAmuletosEquipados()
    {
        foreach(var amuleto in AmuletosEquipados)
        {
            GameObject botonGO = Instantiate(panelEquipadosPrefab, panelDisponible);
            botonGO.GetComponentInChildren<Text>().text = amuleto.Name;



            botonGO.GetComponent<Button>().onClick.AddListener(() =>
            {
                amuletoManager.QuitarAmuleto(amuleto);
                MostrarAmuletosEquipados();
            });


        }
    }
}
