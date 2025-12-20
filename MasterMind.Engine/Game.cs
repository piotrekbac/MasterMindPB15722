using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;

// Piotr Bacior - MasterMind Game Engine - nr indeksu: 15 722 - WSEI Kraków

namespace MasterMind.Engine
{
    // Tworzymy reprezentację wyniku zgadywania kolorów w grze (trafienia dokładne oraz niedokładne)
    public struct GuessResult
    {
        public int ExactMatches { get; set; }           // liczba trafionych kolorów na właściwych pozycjach
        public int PartialMatches { get; set; }         // liczba trafionych kolorów na niewłaściwych pozycjach
        public bool isVictory => ExactMatches == 4;     // zakładamy, 4 kolory do odgadnięcia dla standardowej gry

        // Konstruktor do inicjalizacji wyniku zgadywania - dokładne i niedokładne trafienia
        public GuessResult(int exact, int partial)
        {
            ExactMatches = exact;
            PartialMatches = partial;
        }
    }

    // Tworzymy klasę reprezentującą logikę gry MasterMind 
    public class Game
    {
        // Konfiguracja gry - dozwolone kolory, długość zgadywanych pozycji, maksymalna liczba prób
        private readonly char[] _allowedColors;
        private readonly int _codeLength;
        private readonly int _maxAttempts;

        // Stan naszej gry
        private char[] _secretCode;                         // kod do odgadnięcia (w znaczeniu pozycji do odgadnięcia)
        public int AttemptsUsed { get; private set; }       // liczba wykorzystanych prób
        public bool isGameOver { get; private set; }        // czy gra się zakończyła
        public bool isGameWon { get; private set; }         // czy gra została wygrana


        // Historia gry - przechowujemy zgadywania i ich wyniki w formie ala snapshot'u stanu gry
        public List<(string Guess, GuessResult Result)> History { get; private set; } // historia zgadywań i ich wyników

        // Konstruktor inicjalizujący grę z domyślnymi parametrami - 6 kolorów, długość kodu do zgadnięcia 4, maksymalnie 9 prób
        public Game(int codeLength = 4, int maxAttempts = 9)
        {
            _allowedColors = new char[] { 'r', 'y', 'g', 'b', 'm', 'c' };   // red, yellow, green, blue, magenta, cyan
            _codeLength = codeLength;
            _maxAttempts = maxAttempts;
            History = new List<(string, GuessResult)>();    
            StartNewGame();  // rozpoczynamy nową grę przy tworzeniu instancji
        }

        // Metoda rozpoczynająca nową grę - generuje losowy kod do odgadnięcia i resetuje stan gry
        public void StartNewGame()
        {
            AttemptsUsed = 0;                       // ustawiamy liczbę wykorzystanych prób na 0
            isGameOver = false;                     // resetujemy stan zakończenia gry
            isGameWon = false;                      // resetujemy stan wygranej
            History.Clear();                        // czyścimy historię zgadywań
            _secretCode = GenerateSecretCode();     // generujemy nowy kod do odgadnięcia
        }

        // Metoda do przetwarzania zgadywania gracza - generowanie losowego kodu do odgadnięcia (wariacja z powtórzeniami)
        private char[] GenerateSecretCode()
        {
            // Generujemy losowy kod z dozwolonych kolorów
            Random rnd = new Random();

            // Tworzymy tablicę znaków reprezentującą kod do odgadnięcia
            char[] code = new char[_codeLength];

            // Losujemy kolory dla każdej pozycji w kodzie
            for (int i = 0; i < code.Length; i ++)
            {
                // Wybieramy losowy kolor z dozwolonych kolorów
                code[i] = _allowedColors[rnd.Next(_allowedColors.Length)];
            }

            // Zwracamy wygenerowany kod
            return code;
        }

    }
    
}
