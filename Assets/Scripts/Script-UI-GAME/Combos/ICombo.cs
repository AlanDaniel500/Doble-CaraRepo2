using System.Collections.Generic;
using CardSystem;

public interface ICombo
{
    bool CheckCombo(List<CardData> cartas);
    string Nombre { get; }
    int Prioridad { get; }
    int CalcularDaño(List<CardData> cartas);
}
