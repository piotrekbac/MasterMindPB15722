using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Piotr Bacior - MasterMind Game Engine - nr indeksu: 15 722 - WSEI Kraków

namespace MasterMind.Engine
{
    //// =-=-=-=-=-=--=-=-=-= ZADANIE 1 =-=-=-=-=-=--=-=-=-= 

    //// Klasa odpowiedzialna za logikę rozwiązywania kodu przez komputer
    //public class ComputerSolver
    //{
    //    // Kolory dostępne w grze MasterMind
    //    private readonly char[] _colors = {'r', 'y', 'g', 'b', 'm', 'c' };
    //    private List<string> _possibleCodes;            // Lista możliwych kodów z pliku pdf 
    //    public string LastGuess { get; private set; }   // Ostatnia propozycja komputera
    //    public int MoveCount { get; private set; }      // Liczba ruchów wykonanych przez komputer

    //    // Tworzymy metodę ComputerSolver, która wykonuje metodę Reset
    //    public ComputerSolver()
    //    {
    //        Reset();                                    // Inicjalizacja poprzez reset
    //    }

    //    // Definiujemy metodę Reset która generuje wszystkie możliwe kody i resetuje licznik ruchów oraz ostatnią propozycję
    //    public void Reset()
    //    {
    //        // Generowanie wszystkich możliwych kodów
    //        _possibleCodes = GenerateAllCodes();

    //        MoveCount = 0;                              // Resetowanie licznika ruchów
    //        LastGuess = null;                           // Resetowanie ostatniej propozycji
    //    }

    //    // Definiujemy Listę stringów GenerateAllPossibleCodes która generuje wszystkie możliwe kody (będzie to 1296 kodów)
    //    private List<string> GenerateAllCodes()
    //    {
    //        // Generowanie wszystkich możliwych kodów (6 kolorów, 4 pozycje)
    //        var codes = new List<string>();

    //        // Generowanie kombinacji kodów
    //        foreach (var c1 in _colors)
    //            foreach (var c2 in _colors)
    //                foreach (var c3 in _colors)
    //                    foreach (var c4 in _colors)
    //                    {
    //                        codes.Add($"{c1}{c2}{c3}{c4}");     // Dodawanie kombinacji do listy
    //                    }

    //        // Zwracamy listę wszystkich możliwych kodów
    //        return codes;

    //    }

    //    // Definiujemy metodę GetNextGuess która zwraca następną propozycję komputera
    //    public string GetNextGuess()
    //    {
    //        // Wybieramy pierwszą propozycję z listy możliwych kodów
    //        if (_possibleCodes.Count == 0)
    //        {
    //            // Jeśli nie ma możliwych kodów, zgłaszamy wyjątek
    //            throw new InvalidOperationException("WykrytoOszustwo: Brak pasujących kodów! Użytkownik musiał podać błędną ocenę wcześniej.");
    //        }

    //        // Zwiększamy licznik ruchów
    //        MoveCount++;                  

    //        // Statystycznie najlepsze otwarcie, czyli pierwszy ruch zawsze typu "rrry" bądź podobne
    //        if (MoveCount == 1)
    //        {
    //            LastGuess = "rrry";     // Klasyczne otwarcie 

    //            // Jeżeli "rrry" zostało wykluczone, weźmy pierwszą pozycję z listy 
    //            if (!_possibleCodes.Contains(LastGuess)) LastGuess = _possibleCodes[0];
    //        } 
    //        else
    //        {
    //            // W przeciwnym razie, wybierzmy pierwszą pozycję z listy 
    //            LastGuess = _possibleCodes[0];
    //        }

    //        // Zwracamy ostatnie zgadywanie 
    //        return LastGuess;

    //    }

    //    // Definiujemy metodę ProcessFeedback która przetwarza informację zwrotną od użytkownika
    //    public void ProcessFeedback(int exact, int partial)
    //    {
    //        // Tworzymy wynik informacji zwrotnej na podstawie dokładnych i częściowych trafień
    //        var feedbackResult = new GuessResult(exact, partial);

    //        // Filtrujemy listę możliwych kodów na podstawie informacji zwrotnej
    //        _possibleCodes = _possibleCodes
    //            .Where(potencialSecret => Game.CalculateScore(potencialSecret, LastGuess) == feedbackResult)
    //            .ToList();

    //        /* 

    //         * KRÓTKIE WYTŁUMACZENIE LOGIKI NASZEGO DZIAŁANIA W TYM KODZIE * 

    //        Przykładowo komputer strzelił "rygb". Użytkownik odpowiedział (1,1) czyli 1 kolor na właściwej pozycji i 1 kolor na niewłaściwej pozycji.
    //        Komputer musi teraz wyeliminować wszystkie kody, które nie mogą dać takiego wyniku. Czyli kod "mmmm" wylatuje bo ocena 
    //        ("mmmm", "rygb") to (0,0). Kod "ryyy" też wylatuje bo ocena ("ryyy", "rygb") to (2,0). Kod "rggg" wylatuje bo ocena ("rggg", "rygb") to (1,0).
    //        Czyli kod który pasuje to np "rgyy" bo ocena ("rgyy", "rygb") to (1,1). W ten sposób komputer zawęża listę możliwych kodów do odgadnięcia.


    //         */
    //    }

    //    // Definiujemy metodę, która zwraca nam liczbę pozostałych możliwości pozycji do zgadnięcia
    //    public int ReminingPossibilities => _possibleCodes.Count;

    //}

    // =-=-=-=-=-=--=-=-=-= ZADANIE 2 =-=-=-=-=-=--=-=-=-= 

    // Klasa odpowiedzialna za logikę rozwiązywania kodu przez komputer - wersja uproszczona
    public class ComputerSolver
    {
        private char[] _colors;         // Kolory dostępne w grze MasterMind
        private int _codeLength;        // Długość kodu do odgadnięcia
        private List<string> _possibleCodes;    // Lista możliwych kodów
        public string LastGuess { get; private set; }   // Ostatnia propozycja komputera
        public int MoveCount { get; private set; }      // Liczba ruchów wykonanych przez komputer
        public int CurrentN { get; private set; }      // Aktualna wartość N (ilość kolorów)
        public int CurrentK { get; private set; }      // Aktualna wartość K (długość kodu)


        // Tworzymy metodę ComputerSolver, która przyjmuje parametry n i k oraz wykonuje metodę Reset
        public ComputerSolver(int n = 6, int k = 4)
        {
            var tempGame = new Game(n, k);    // Tworzymy tymczasową grę aby pobrać kolory i długość kodu
            _colors = tempGame.Colors;        // Pobieramy kolory z gry
            _codeLength = tempGame.CodeLength; // Pobieramy długość kodu z gry
            CurrentN = n;                     // Ustawiamy aktualną wartość N
            CurrentK = k;                     // Ustawiamy aktualną wartość K
            Reset();                          // Inicjalizacja poprzez reset
        }

        // Definiujemy metodę Reset która generuje wszystkie możliwe kody i resetuje licznik ruchów oraz ostatnią propozycję
        public void Reset()
        { 
            // Generowanie wszystkich możliwych kodów rekurencyjnie
            _possibleCodes = GeneratedAllCodesRecursively();   

            MoveCount = 0;              // Resetowanie licznika ruchów
            LastGuess = null;           // Resetowanie ostatniej propozycji
        }
    }
}
