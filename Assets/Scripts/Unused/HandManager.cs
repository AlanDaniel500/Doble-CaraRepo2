using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

/*public class HandManager : MonoBehaviour
{
    [Header("Configuración de mano")]
    [SerializeField] private int maxHandSize = 8; // Número máximo de cartas por jugador
    [SerializeField] private GameObject cardPrefab; 

    [Header("Referencias de Splines")]
    [SerializeField] private SplineContainer playerSpline; // Spline donde se colocan las cartas del jugador
    [SerializeField] private SplineContainer enemySpline;  // Spline donde se colocan las cartas del enemigo

    [Header("Punto de salida de las cartas")]
    [SerializeField] private Transform spawnPoint; 

    // Listas para almacenar las cartas instanciadas de cada jugador
    private List<GameObject> playerCards = new();
    private List<GameObject> enemyCards = new();

    private bool cardsDealt = false; // Evita repartir cartas múltiples veces en Update

    private void Update()
    {
        // Reparte cartas una sola vez cuando empieza el juego
        if (!cardsDealt)
        {
            StartCoroutine(DealCardsCoroutine()); 
            cardsDealt = true;
        }

        // Prueba: presioná "T" para agregar una carta al jugador en cualquier momento
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddCardToPlayerHand();
        }

    }

    private IEnumerator DealCardsCoroutine()
    {
        int totalCards = maxHandSize * 2; // 8 para jugador + 8 para enemigo

        for (int i = 0; i < totalCards; i++)
        {
            // Alterna el reparto: true = jugador, false = enemigo
            //Si i es par el resultado será verdadero.
            //Si i es impar el resultado será falso.
            bool isPlayerTurn;

            if (i % 2 == 0)
            {
                isPlayerTurn = true;
            }
            else
            {
                isPlayerTurn = false;
            }

            if (isPlayerTurn && playerCards.Count < maxHandSize)
            {
                // Instancia la carta y la agrega a la mano del jugador
                GameObject card = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
                playerCards.Add(card);

                // Reacomoda visualmente las cartas en su spline
                UpdateCardPositions(playerCards, playerSpline);
            }
            else if (!isPlayerTurn && enemyCards.Count < maxHandSize)
            {
                // Instancia la carta y la agrega a la mano del enemigo
                GameObject card = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
                enemyCards.Add(card);

                // Reacomoda visualmente las cartas en su spline
                UpdateCardPositions(enemyCards, enemySpline);
            }

            yield return new WaitForSeconds(0.2f); // Espera entre cartas para simular reparto animado
        }
    }

    public void AddCardToPlayerHand()
    {
        GameObject card = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        playerCards.Add(card);
        UpdateCardPositions(playerCards, playerSpline);

        Debug.Log("Carta agregada manualmente. Total: " + playerCards.Count);
    }

    /// <summary>
    /// Acomoda las cartas a lo largo del spline, con animación usando DOTween.
    /// </summary>
    /// <param name="cards">Lista de cartas a posicionar</param>
    /// <param name="splineContainer">SplineContainer que define la curva de la mano</param>
    private void UpdateCardPositions(List<GameObject> cards, SplineContainer splineContainer)
    {
        if (cards.Count == 0) return;

        float spacing = 1f / maxHandSize; // Distancia uniforme entre cartas (en el espacio de la spline)
        float start = 0.5f - (cards.Count - 1) * spacing / 2f; // Punto de inicio centrado

        Spline spline = splineContainer.Spline; // Obtenemos la spline del contenedor

        for (int i = 0; i < cards.Count; i++)
        {
            float t = start + i * spacing; // Posición normalizada (0 a 1) a lo largo de la spline

            // Obtenemos posición y dirección en la spline (¡EN LOCAL!)
            Vector3 localPos = spline.EvaluatePosition(t);
            Vector3 localForward = spline.EvaluateTangent(t);
            Vector3 localUp = spline.EvaluateUpVector(t);

            // Convertimos a coordenadas del mundo usando el transform del spline
            Vector3 worldPos = splineContainer.transform.TransformPoint(localPos);
            Vector3 worldForward = splineContainer.transform.TransformDirection(localForward);
            Vector3 worldUp = splineContainer.transform.TransformDirection(localUp);

            // Calculamos rotación de la carta alineada al spline
            Quaternion rotation = Quaternion.LookRotation(worldUp, Vector3.Cross(worldUp, worldForward).normalized);

            // Animación: mueve y rota cada carta con suavidad
            cards[i].transform.DOMove(worldPos, 0.3f).SetEase(Ease.OutQuad);
            cards[i].transform.DOLocalRotateQuaternion(rotation, 0.3f).SetEase(Ease.OutQuad);

            // (opcional) Debug: mostrar flechas en el editor
            // Debug.DrawRay(worldPos, worldForward * 0.3f, Color.green, 2f);
        }
    }
}*/