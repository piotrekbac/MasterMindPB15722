using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Piotr Bacior - MasterMind Game Engine - nr indeksu: 15 722 - WSEI Kraków

namespace MasterMind.Engine
{
    // Klasa odpowiedzialna za logikę rozwiązywania kodu przez komputer
    public class ComputerSolver
    {
        // Kolory dostępne w grze MasterMind
        private readonly char[] _colors = {'r', 'y', 'g', 'b', 'm', 'c' };
        private List<string> _possibleCodes;            // Lista możliwych kodów z pliku pdf 
        public string LastGuess { get; private set; }   // Ostatnia propozycja komputera
        public int MoveCount { get; private set; }      // Liczba ruchów wykonanych przez komputer

        // Tworzymy metodę ComputerSolver, która wykonuje metodę Reset
        public ComputerSolver()
        {
            Reset();                                    // Inicjalizacja poprzez reset
        }

        // Definiujemy metodę Reset która generuje wszystkie możliwe kody i resetuje licznik ruchów oraz ostatnią propozycję
        public void Reset()
        {
            // Generowanie wszystkich możliwych kodów
            _possibleCodes = GenerateAllPossibleCodes();

            MoveCount = 0;                              // Resetowanie licznika ruchów
            LastGuess = null;                           // Resetowanie ostatniej propozycji
        }

        // Definiujemy Listę stringów GenerateAllPossibleCodes która generuje wszystkie możliwe kody (będzie to 1296 kodów)
        private List<string> GenerateAllCodes()
        {
            // Generowanie wszystkich możliwych kodów (6 kolorów, 4 pozycje)
            var codes = new List<string>();

            // Generowanie kombinacji kodów
            foreach (var c1 in _colors)
                foreach (var c2 in _colors)
                    foreach (var c3 in _colors)
                        foreach (var c4 in _colors)
                        {
                            codes.Add($"{c1}{c2}{c3}{c4}");     // Dodawanie kombinacji do listy
                        }

            // Zwracamy listę wszystkich możliwych kodów
            return codes;

        }

        // Definiujemy metodę GetNextGuess która zwraca następną propozycję komputera
        public string GetNextGuess()
        {
            // Wybieramy pierwszą propozycję z listy możliwych kodów
            if (_possibleCodes == 0)
            {
                // Jeśli nie ma możliwych kodów, zgłaszamy wyjątek
                throw new InvalidOperationException("WykrytoOszustwo: Brak pasujących kodów! Użytkownik musiał podać błędną ocenę wcześniej.");
            }

            // Zwiększamy licznik ruchów
            MoveCount++;                  
            
            // Statystycznie najlepsze otwarcie, czyli pierwszy ruch zawsze typu "rrry" bądź podobne
            if (MoveCount == 1)
            {
                LastGuess = "rrry";     // Klasyczne otwarcie 

                // Jeżeli "rrry" zostało wykluczone, weźmy pierwszą pozycję z listy 
                if (!_possibleCodes.Contains(LastGuess)) LastGuess = _possibleCodes[0];
            } 
            else
            {
                // W przeciwnym razie, wybierzmy pierwszą pozycję z listy 
                LastGuess = _possibleCodes[0];
            }



        }

      
    }
}
