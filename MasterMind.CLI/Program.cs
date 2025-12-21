using System;
using MasterMind.Engine;

// Piotr Bacior - MasterMind Game - nr indeksu: 15 722 - WSEI Kraków

namespace MasterMind.CLI
    {
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();  // tworzymy nową grę MasterMind

            Console.WriteLine("-_-_-_-_-_ GRA MASTER MIND -_-_-_-_-_\n");
            Console.WriteLine("Autor: Piotr Bacior 15 722\n");
            Console.WriteLine("Twoim zadaniem jest odgadnąć kod składający się z 4 kolorów");
            Console.WriteLine($"Dostępne kolory: {game.GetAllowedColors()}");
            Console.WriteLine("Oznaczenia wyniku: [X,Y] gdzie X to idealne trafienia (czarne), a Y to zła pozycja (białe).\n");
            Console.WriteLine("---PB15722------PB15722------PB15722---");

            // Główna pętla gry - kontynuujemy aż do zakończenia gry
            while (!game.isGameOver)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Próba {game.AttemptsUsed + 1}");
                Console.ResetColor();

                string input = Console.ReadLine()?.Trim().ToLower();
            }


        }
    }
}