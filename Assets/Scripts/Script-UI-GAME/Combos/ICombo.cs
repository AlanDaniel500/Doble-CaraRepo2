using System.Collections.Generic;
using CardSystem;

// Interfaz base para todos los combos
public interface ICombo
{
    bool CheckCombo(List<CardData> cartas);
    string Nombre { get; }
    int Prioridad { get; }
    int CalcularDaño(List<CardData> cartas);
}

// Interfaz opcional: solo la implementan combos con efectos especiales
public interface IComboConEfecto
{
    void AplicarEfecto(List<CardData> cartas, Player player);
}
