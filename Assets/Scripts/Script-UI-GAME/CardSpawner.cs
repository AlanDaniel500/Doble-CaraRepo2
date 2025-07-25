using UnityEngine;
using CardSystem;
using System.Collections;
using System.Collections.Generic;

public class CardSpawner : MonoBehaviour
{
    public string cardDataFolder = "Cards";
    public int cartasIniciales = 7;

    [Tooltip("Posiciones donde se mostrarán las cartas (editable en inspector)")]
    public List<Vector3> posicionesCartas = new List<Vector3>();

    [Header("Animación de Flip")]
    [Tooltip("Sprite del dorso de la carta")]
    public Sprite dorsoCarta;

    [Tooltip("Tiempo antes de que la carta se dé vuelta")]
    public float flipDelay = 0.5f;

    private List<GameObject> cartasEnJuego = new List<GameObject>();

    private void Start()
    {
        if (posicionesCartas == null || posicionesCartas.Count < 7)
        {
            posicionesCartas = new List<Vector3>();
            float startX = -17.2443f;
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
        cartaGO.transform.localScale = new Vector3(0.7898855f, 0.9101233f, 0f);

        var sr = cartaGO.AddComponent<SpriteRenderer>();
        sr.sprite = dorsoCarta;

        cartaGO.AddComponent<BoxCollider2D>();

        var info = cartaGO.AddComponent<CardInfo>();
        info.data = data;

        cartaGO.AddComponent<CardSelector>();

        cartasEnJuego.Add(cartaGO);

        // Cambiar sprite al frente después del delay
        StartCoroutine(FlipCardAfterDelay(sr, data.cardImage));
    }

    IEnumerator FlipCardAfterDelay(SpriteRenderer sr, Sprite spriteFinal)
    {
        yield return new WaitForSeconds(flipDelay);
        sr.sprite = spriteFinal;
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
