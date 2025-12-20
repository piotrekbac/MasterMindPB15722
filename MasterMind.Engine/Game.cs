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

        public GuessResult(int exact, int partial)
        {
            ExactMatches = exact;
            PartialMatches = partial;
        }
    }


    
}
