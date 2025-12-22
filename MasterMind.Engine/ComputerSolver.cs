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

       
    }
}
