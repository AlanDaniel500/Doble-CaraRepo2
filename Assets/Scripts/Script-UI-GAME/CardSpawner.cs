using UnityEngine;
using CardSystem;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    public string cardDataFolder = "Cards";
    public int cartasIniciales = 8;

    [Tooltip("Posiciones donde se mostrarán las cartas (editable en inspector)")]
    public List<Vector3> posicionesCartas = new List<Vector3>();

    private List<GameObject> cartasEnJuego = new List<GameObject>();

    private void Start()
    {
        // Si no definiste las posiciones en inspector, crea 8 por defecto
        if (posicionesCartas == null || posicionesCartas.Count < 8)
        {
            posicionesCartas = new List<Vector3>();
            float startX = -10.03f;
            float y = -7.82f;
            float offsetX = 2f;

            for (int i = 0; i < 8; i++)
            {
                posicionesCartas.Add(new Vector3(startX + i * offsetX, y, 0f));
            }
        }

        CargarCartasIniciales();
    }

    void CargarCartasIniciales()
    {
        CardData[] todasLasCartas = Resources.LoadAll<CardData>(cardDataFolder);

        if (todasLasCartas.Length == 0)
        {
            Debug.LogWarning("No se encontraron cartas en Resources/" + cardDataFolder);
            return;
        }

        for (int i = 0; i < cartasIniciales && i < posicionesCartas.Count; i++)
        {
            CardData carta = todasLasCartas[Random.Range(0, todasLasCartas.Length)];
            CrearCartaEnMundo(carta, posicionesCartas[i]);
        }
    }

    void CrearCartaEnMundo(CardData data, Vector3 posicion)
    {
        GameObject cartaGO = new GameObject("Carta_" + data.cardName);
        cartaGO.transform.position = posicion;

        var sr = cartaGO.AddComponent<SpriteRenderer>();
        sr.sprite = data.cardImage;

        cartaGO.AddComponent<BoxCollider2D>();

        var info = cartaGO.AddComponent<CardInfo>();
        info.data = data;

        cartaGO.AddComponent<CardSelector>();

        cartasEnJuego.Add(cartaGO);
    }

    int ObtenerIndiceLibre()
    {
        for (int i = 0; i < posicionesCartas.Count; i++)
        {
            bool ocupado = false;
            foreach (var carta in cartasEnJuego)
            {
                if (Vector3.Distance(carta.transform.position, posicionesCartas[i]) < 0.1f)
                {
                    ocupado = true;
                    break;
                }
            }
            if (!ocupado)
                return i;
        }

        return -1;
    }

    public int CantidadCartasEnJuego()
    {
        return cartasEnJuego.Count;
    }

    public void RepartirCartaIndividual()
    {
        int indiceLibre = ObtenerIndiceLibre();

        if (indiceLibre == -1)
        {
            Debug.Log("Ya tenés 8 cartas, no podés agarrar más.");
            return;
        }

        CardData[] todasLasCartas = Resources.LoadAll<CardData>(cardDataFolder);

        if (todasLasCartas.Length == 0)
        {
            Debug.LogWarning("No hay cartas en Resources/" + cardDataFolder);
            return;
        }

        CardData carta = todasLasCartas[Random.Range(0, todasLasCartas.Length)];
        CrearCartaEnMundo(carta, posicionesCartas[indiceLibre]);
    }

    public bool HayCartasSeleccionadas()
    {
        foreach (var carta in cartasEnJuego)
        {
            CardSelector selector = carta.GetComponent<CardSelector>();
            if (selector != null && selector.IsSelected())
                return true;
        }
        return false;
    }

    public void EliminarCartasSeleccionadas()
    {
        var cartasParaEliminar = new List<GameObject>();

        foreach (var carta in cartasEnJuego)
        {
            CardSelector selector = carta.GetComponent<CardSelector>();
            if (selector != null && selector.IsSelected())
                cartasParaEliminar.Add(carta);
        }

        foreach (var carta in cartasParaEliminar)
        {
            cartasEnJuego.Remove(carta);
            Destroy(carta);
        }
    }

    // Método nuevo para obtener la lista de cartas seleccionadas con su CardData
    public List<CardData> ObtenerCartasSeleccionadas()
    {
        List<CardData> seleccionadas = new List<CardData>();

        foreach (var cartaGO in cartasEnJuego)
        {
            CardSelector selector = cartaGO.GetComponent<CardSelector>();
            if (selector != null && selector.IsSelected())
            {
                CardInfo info = cartaGO.GetComponent<CardInfo>();
                if (info != null && info.data != null)
                {
                    seleccionadas.Add(info.data);
                }
            }
        }

        return seleccionadas;
    }
}
