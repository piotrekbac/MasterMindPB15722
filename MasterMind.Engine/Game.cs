using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;

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

       

    }
    
}
