using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    //    public class ComputerSolver
    //    {
    //        private char[] _colors;         // Kolory dostępne w grze MasterMind
    //        private int _codeLength;        // Długość kodu do odgadnięcia
    //        private List<string> _possibleCodes;    // Lista możliwych kodów
    //        public string LastGuess { get; private set; }   // Ostatnia propozycja komputera
    //        public int MoveCount { get; private set; }      // Liczba ruchów wykonanych przez komputer
    //        public int CurrentN { get; private set; }      // Aktualna wartość N (ilość kolorów)
    //        public int CurrentK { get; private set; }      // Aktualna wartość K (długość kodu)


    //        // Tworzymy metodę ComputerSolver, która przyjmuje parametry n i k oraz wykonuje metodę Reset
    //        public ComputerSolver(int n = 6, int k = 4)
    //        {
    //            var tempGame = new Game(n, k);    // Tworzymy tymczasową grę aby pobrać kolory i długość kodu
    //            _colors = tempGame.GetAllowedColorsArray();        // Pobieramy kolory z gry
    //            _codeLength = tempGame.CodeLength; // Pobieramy długość kodu z gry
    //            CurrentN = n;                     // Ustawiamy aktualną wartość N
    //            CurrentK = k;                     // Ustawiamy aktualną wartość K
    //            Reset();                          // Inicjalizacja poprzez reset
    //        }

    //        // Definiujemy metodę Reset która generuje wszystkie możliwe kody i resetuje licznik ruchów oraz ostatnią propozycję
    //        public void Reset()
    //        { 
    //            // Generowanie wszystkich możliwych kodów rekurencyjnie
    //            _possibleCodes = GeneratedAllCodesRecursively();   

    //            MoveCount = 0;              // Resetowanie licznika ruchów
    //            LastGuess = null;           // Resetowanie ostatniej propozycji
    //        }

    //        // Definiujemy Listę stringów GeneratedAllCodesRecursively która generuje wszystkie możliwe kody rekurencyjnie
    //        private List<string> GeneratedAllCodesRecursively()
    //        {
    //            var results = new List<string>();       // Lista do przechowywania wygenerowanych kodów
    //            GenerateRecursiveStep("", results);     // Wywołanie rekurencyjnej funkcji generującej kody
    //            return results;                         // Zwracamy listę wygenerowanych kodów
    //        }

    //        // Rekurencyjna metoda generująca kody
    //        private void GenerateRecursiveStep(string currentCode, List<string> results)
    //        {
    //            // Jeśli długość bieżącego kodu osiągnęła długość docelową, dodajemy go do wyników
    //            if (currentCode.Length == _codeLength)
    //            {
    //                results.Add(currentCode);       // Dodajemy wygenerowany kod do wyników
    //                return;                         // Kończymy rekurencję dla tego kodu
    //            }
    //            // Rekurencyjnie dodajemy każdy kolor do bieżącego kodu i wywołujemy funkcję ponownie
    //            foreach (var color in _colors)
    //            {
    //                // Dodajemy kolor do bieżącego kodu i wywołujemy rekurencję
    //                GenerateRecursiveStep(currentCode + color, results);
    //            }
    //        }

    //        public string GetNextGuess()
    //        {
    //            // Wybieramy pierwszą propozycję z listy możliwych kodów
    //            if (_possibleCodes.Count == 0)
    //            {
    //                // Jeśli nie ma możliwych kodów, zgłaszamy wyjątek
    //                throw new InvalidOperationException("WykrytoOszustwo: Brak pasujących kodów! Użytkownik musiał podać błędną ocenę wcześniej.");
    //            }
    //            // Zwiększamy licznik ruchów
    //            MoveCount++;

    //            // Statystycznie najlepsze otwarcie, czyli pierwszy ruch zawsze typu "rrry" bądź podobne
    //            if (MoveCount == 1 && _codeLength == 4 && _colors.Length >= 2)
    //            {
    //                // Definiuejmy optymalne otwarcie dla klasycznej gry MasterMind (4 pozycje, co najmniej 2 kolory)
    //                string optimalStart = $"({_colors[0]})({_colors[0]})({_colors[1]})({_colors[1]})";

    //                LastGuess = optimalStart;     // Klasyczne otwarcie

    //                // Jeżeli optymalne otwarcie zostało wykluczone, weźmy pierwszą pozycję z listy
    //                if (!_possibleCodes.Contains(LastGuess)) LastGuess = _possibleCodes[0]; 
    //            }
    //            else
    //            {
    //                LastGuess = _possibleCodes[0];   // W przeciwnym razie, wybierzmy pierwszą pozycję z listy
    //            }

    //            return LastGuess;    // Zwracamy ostatnie zgadywanie
    //        }

    //        // Definiujemy metodę ProcessFeedback która przetwarza informację zwrotną od użytkownika
    //        public void ProcessFeedback(int exact, int partial)
    //        {
    //            // Tworzymy wynik informacji zwrotnej na podstawie dokładnych i częściowych trafień
    //            var feedbackResult = new GuessResult(exact, partial);

    //            // Filtrujemy listę możliwych kodów na podstawie informacji zwrotnej
    //            _possibleCodes = _possibleCodes
    //                .Where(potencialSecret => Game.CalculateScore(potencialSecret, LastGuess) == feedbackResult)
    //                .ToList();
    //        }

    //        // Definiujemy metodę, która zwraca nam liczbę pozostałych możliwości pozycji do zgadnięcia
    //        public int ReminingPossibilities => _possibleCodes.Count;
    //    }
    //}

    // =-=-=-=-=-=--=-=-=-= ZADANIE 4 =-=-=-=-=-=--=-=-=-= 

    // Klasa odpowiedzialna za logikę rozwiązywania kodu przez komputer - wersja z trybem klasycznym i zaawansowanym
    public class ComputerSolver
    {
        private readonly char[] _colors;         // Kolory dostępne w grze MasterMind
        private readonly int _codeLength;        // Długość kodu do odgadnięcia

        private readonly List<string> _allPossibleCodes;    // Lista wszystkich możliwych kodów

        private List<string> _workingSet;            // Lista robocza (używana w trybie klasycznym)

        private List<(string Guess, GuessResult Result)> _history;

        public string LastGuess { get; private set; }   // Ostatnia propozycja komputera
        public int MoveCount { get; private set; }      // Liczba ruchów wykonanych przez komputer

        public bool AllowErrors { get; set; } = false;  // Flaga trybu zaawansowanego (zezwalającego na błędy użytkownika)
        public int MaxErrorsAllowed { get; set; } = 0;  // Maksymalna liczba błędów dozwolonych w trybie zaawansowanym

        public int DetectedErrorsForBestCondidate { get; private set; } = 0; // Liczba wykrytych błędów dla najlepszego kandydata

        public ComputerSolver(int n = 6, int k = 4)
        {
            var tempGame = new Game(n, k);                      // Tworzymy tymczasową grę aby pobrać kolory i długość kodu
            _colors = tempGame.GetAllowedColorsArray();         // Pobieramy kolory z gry
            _codeLength = k;               // Pobieramy długość kodu z gry

            // Generowanie wszystkich możliwych kodów rekurencyjnie
            _allPermutations = GenerateAllCodesRecursively();       // Lista wszystkich możliwych kodów
            _workingSet = new List<string>(_allPermutations);       // Lista robocza inicjalizowana wszystkimi kodami
            _history = new List<(string, GuessResult)>();           // Historia zgadywań i wyników

            Reset();        // Inicjalizacja poprzez reset
        }

        // Definiujemy Listę stringów GenerateAllCodesRecursively która generuje wszystkie możliwe kody rekurencyjnie
        public void Reset()
        {
            // Resetowanie stanu gry
            _workingSet = new List<string>(_allPermutations);   

            _history.Clear();                       // Czyszczenie historii zgadywań
            MoveCount = 0;                          // Resetowanie licznika ruchów
            LastGuess = null;                       // Resetowanie ostatniej propozycji
            DetectedErrorsForBestCondidate = 0;     // Resetowanie liczby wykrytych błędów
        }

        // Definiujemy Listę stringów GenerateAllCodesRecursively która generuje wszystkie możliwe kody rekurencyjnie
        private List<string> GenerateAllCodesRecursively()
        {
            var results = new List<string>();       // Lista do przechowywania wygenerowanych kodów
            GenerateRecursiveStep("", results);     // Wywołanie rekurencyjnej funkcji generującej kody
            return results;                         // Zwracamy listę wygenerowanych kodów
        }

        // Definiujemy rekurencyjną metodę generującą kody
        private void GenerateRecursiveStep(string currentCode, List<string> results)
        {
            // Jeśli długość bieżącego kodu osiągnęła długość docelową, dodajemy go do wyników
            if (currentCode.Length == _codeLength)
            {
                results.Add(currentCode);       // Dodajemy wygenerowany kod do wyników
                return;                         // Kończymy rekurencję dla tego kodu
            }
            // Rekurencyjnie dodajemy każdy kolor do bieżącego kodu i wywołujemy funkcję ponownie
            foreach (var color in _colors)
            {
                // Dodajemy kolor do bieżącego kodu i wywołujemy rekurencję
                GenerateRecursiveStep(currentCode + color, results);
            }
        }

        // Definiujemy metodę GetNextGuess która zwraca następną propozycję komputera
        public string GetNextGuess()
        {
            MoveCount++;                   // Zwiększamy licznik ruchów

            // Wybieramy pierwszą propozycję z listy możliwych kodów
            if (MoveCount == 1)
            {
                // Statystycznie najlepsze otwarcie, czyli pierwszy ruch zawsze typu "rryy" bądź podobne
                if (_codeLength >= 4 && _colors.Length >= 2)
                {
                    LastGuess = $"({_colors[0]})({_colors[0]})({_colors[1]})({_colors[1]})" + (_codeLength > 4 ? new string(_colors[0], _codeLength - 4) : ""); // Optymalne otwarcie dla klasycznej gry MasterMind
                }

                // Jeżeli optymalne otwarcie zostało wykluczone, weźmy pierwszą pozycję z listy roboczej
                else
                {
                    // Jeżeli optymalne otwarcie nie jest możliwe, wybieramy pierwszą pozycję z listy roboczej
                    LastGuess = _allPossibleCodes[0]; 
                }

                // Jeżeli optymalne otwarcie zostało wykluczone, weźmy pierwszą pozycję z listy roboczej
                if (!AllowErrors && !_workingSet.Contains(LastGuess)) LastGuess = _workingSet[0]; 
                {
                    // W przeciwnym razie, wybieramy pierwszą pozycję z listy roboczej
                    return LastGuess;    
                }
            }

            // Tryb klasyczny - bez błędów, szybsze działanie i usuwanie
            if (!AllowErrors)
            {
                // Filtrujemy listę roboczą na podstawie historii zgadywań
                if (_workingSet.Count == 0)
                {
                    throw new InvalidOperationException("Brak pasujących kodów! Sprzeczność w odpowiedziach.");
                }

                LastGuess = _workingSet[0];     // Wybieramy pierwszą pozycję z listy roboczej
                return LastGuess;               // Zwracamy ostatnie zgadywanie
            }

            // Tryb zaawansowany - zezwalający na błędy użytkownika, wolniejsze działanie, analiza historii
            else
            {
                // Analizujemy wszystkie możliwe kody i wybieramy te, które minimalizują liczbę sprzeczności z historią zgadywań
                var candidates = new List<string>();

                // Najmniejsza liczba błędów znaleziona jak dotąd
                int minErrorsFound = int.MaxValue;

                // Przeglądamy wszystkie możliwe kody
                foreach (var potencialCode in _allPermutations)
                {
                    // Liczymy liczbę błędów dla tego potencjalnego kodu w stosunku do historii zgadywań
                    int errors = 0;

                    // Sprawdzamy każdy wpis w historii zgadywań
                    foreach (var entry in _history)
                    {
                        // Symulujemy wynik dla potencjalnego kodu i porównujemy z rzeczywistym wynikiem
                        var simulatedResult = Game.CalculateScore(potencialCode, entry.Guess);

                        // Jeśli wynik się nie zgadza, zwiększamy licznik błędów
                        if (simulatedResult != entry.Result)
                        {
                            errors++;   // Zwiększamy liczbę błędów
                        }

                        // Przerywamy, jeśli liczba błędów przekracza już najlepszy znaleziony wynik
                        if (errors > minErrorsFound && minErrorsFound != int.MaxValue)
                        {
                            break;
                        }
                    }

                    // Aktualizujemy listę kandydatów, jeśli znaleźliśmy mniej błędów
                    if (errors < minErrorsFound)
                    {
                        minErrorsFound = errors;            // Aktualizujemy minimalną liczbę błędów
                        candidates.Clear();                 // Czyścimy listę kandydatów
                        candidates.Add(potencialCode);      // Dodajemy nowego kandydata
                    }

                    // Jeśli liczba błędów jest równa minimalnej znalezionej, dodajemy do kandydatów
                    else if (errors == minErrorsFound)
                    {
                        candidates.Add(potencialCode);      // Dodajemy kandydata do listy
                    }
                }

                // Ustawiamy liczbę wykrytych błędów dla najlepszego kandydata
                DetectedErrorsForBestCondidate = minErrorsFound;

                // Sprawdzamy, czy liczba błędów przekracza dozwolony limit
                if (minErrorsFound > MaxErrorsAllowed)
                {
                    throw new InvalidOperationException($"Nawet najlepsze pasujące kody mają {minErrorsFound} sprzeczności, a zezwoliłeś na max {MaxErrorsAllowed}. Za dużo kłamstw!");
                }

                LastGuess = candidates[0];      // Wybieramy pierwszego kandydata jako następne zgadywanie
                return LastGuess;               // Zwracamy ostatnie zgadywanie
            }   
        }

        // Definiujemy metodę ProcessFeedback która przetwarza informację zwrotną od użytkownika
        public void ProcessFeedback(int exact, int partial)
        {
            var feedbackResult = new GuessResult(exact, partial);   // Tworzymy wynik informacji zwrotnej na podstawie dokładnych i częściowych trafień

            // Dodajemy zgadywanie i wynik do historii
            _history.Add((LastGuess, feedbackResult));

            // Tryb klasyczny - bez błędów, szybsze działanie i usuwanie
            if (!AllowErrors)
            {
                // Filtrujemy listę roboczą na podstawie informacji zwrotnej
                _workingSet = _workingSet
                    .Where(potentialSecret => Game.CalculateScore(potentialSecret, LastGuess) == feedbackResult)
                    .ToList();
            }
        }

        // Definiujemy metodę, która zwraca nam liczbę pozostałych możliwości pozycji do zgadnięcia
        public int GetReminingPossibilities()
        {
            // Tryb klasyczny - bez błędów 
            if (!AllowErrors)
            {
                return _workingSet.Count;       // Zwracamy liczbę pozostałych możliwości w trybie klasycznym
            }

            // W trybie zaawansowanym, zwracamy liczbę wszystkich możliwych kodów
            return -1;
        }
    }
}
