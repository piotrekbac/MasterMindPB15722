using System;
using MasterMind.Engine;

// Piotr Bacior - MasterMind Game - nr indeksu: 15 722 - WSEI Kraków

namespace MasterMind.CLI
    {
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();         // tworzymy nową grę MasterMind

            Console.WriteLine("-_-_-_-_-_ GRA MASTER MIND -_-_-_-_-_\n");
            Console.WriteLine("Autor: Piotr Bacior 15 722\n");
            Console.WriteLine("Twoim zadaniem jest odgadnąć kod składający się z 4 kolorów");
            Console.WriteLine($"Dostępne kolory: {game.GetAllowedColors()}");
            Console.WriteLine("Oznaczenia wyniku: [X,Y] gdzie X to idealne trafienia (czarne), a Y to zła pozycja (białe).\n");
            Console.WriteLine("---PB15722------PB15722------PB15722---\n");

            // Główna pętla gry - kontynuujemy aż do zakończenia gry
            while (!game.isGameOver)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Próba {game.AttemptsUsed + 1}\n");
                Console.ResetColor();

                // Pobieramy dane wejściowe od użytkownika 
                string input = Console.ReadLine()?.Trim().ToLower();

                // Walidacja danych wejściowych - sprawdzamy czy wprowadzono dokładnie 4 litery
                if (string.IsNullOrEmpty(input) || input.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nieprawidłowe dane wejściowe. Wprowadź dokładnie 4 litery reprezentujące kolory.\n");
                    Console.ResetColor();   
                    continue;                // przechodzimy do następnej iteracji pętli
                }

                // Funkcja do wyświetlania wyniku zgadywania 
                try
                {
                    var result = game.EvaluateGuess(input);         // oceniamy zgadywanie użytkownika

                    DisplayResult(result);                          // wyświetlamy wynik zgadywania
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}\n");
                }
            }
        }

        // Funkcja do wyświetlania wyniku zgadywania w czytelny sposób
        static void DisplayResult(GuessResult result)
        {
            // Wyświetlamy wynik zgadywania za pomocą symboli i ustawiamy odpowiedni kolor tła konsoli
            Console.WriteLine("Wynik:\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;

            // Wyświetlamy idealne trafienia i trafienia na złej pozycji
            for (int i = 0; i < result.ExactMatches; i++)
            {
                Console.Write("* ");     // idealne trafienia
            }

            // ustawiamy kolor dla trafień na złej pozycji i wyświetlamy je 
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < result.PartialMatches; i++)
            {
                Console.Write("o ");     // trafienia na złej pozycji
            }

            // Resetujemy kolor konsoli do domyślnego
            Console.ResetColor();

            // Wyświetlamy dokładne liczby trafień i trafień na złej pozycji`
            Console.WriteLine($" (Dokładne: {result.ExactMatches}, Zła pozycja: {result.PartialMatches})\n");
        }
    }
}