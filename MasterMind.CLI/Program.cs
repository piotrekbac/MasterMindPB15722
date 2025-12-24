////=-=-=-=-=-= --=-=-=-= ZADANIE 1 =-=-=-=-=-= --=-=-=-=

//using System;
//using MasterMind.Engine;

//// Piotr Bacior - MasterMind Game - nr indeksu: 15 722 - WSEI Kraków

//namespace MasterMind.CLI
//    {
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Game game = new Game();         // tworzymy nową grę MasterMind

//            Console.WriteLine("-_-_-_-_-_ GRA MASTER MIND -_-_-_-_-_\n");
//            Console.WriteLine("Autor: Piotr Bacior 15 722\n");
//            Console.WriteLine("Twoim zadaniem jest odgadnąć kod składający się z 4 kolorów");
//            Console.WriteLine($"Dostępne kolory: {game.GetAllowedColors()}");
//            Console.WriteLine("Oznaczenia wyniku: [X,Y] gdzie X to idealne trafienia (czarne), a Y to zła pozycja (białe).\n");
//            Console.WriteLine("---PB15722------PB15722------PB15722---\n");

//            // Główna pętla gry - kontynuujemy aż do zakończenia gry
//            while (!game.isGameOver)
//            {
//                Console.ForegroundColor = ConsoleColor.DarkCyan;
//                Console.WriteLine($"Próba {game.AttemptsUsed + 1}\n");
//                Console.ResetColor();

//                // Pobieramy dane wejściowe od użytkownika 
//                string input = Console.ReadLine()?.Trim().ToLower();

//                // Walidacja danych wejściowych - sprawdzamy czy wprowadzono dokładnie 4 litery
//                if (string.IsNullOrEmpty(input) || input.Length != 4)
//                {
//                    Console.ForegroundColor = ConsoleColor.Red;
//                    Console.WriteLine("Nieprawidłowe dane wejściowe. Wprowadź dokładnie 4 litery reprezentujące kolory.\n");
//                    Console.ResetColor();   
//                    continue;                // przechodzimy do następnej iteracji pętli
//                }

//                // Funkcja do wyświetlania wyniku zgadywania 
//                try
//                {
//                    var result = game.EvaluateGuess(input);         // oceniamy zgadywanie użytkownika

//                    DisplayResult(result);                          // wyświetlamy wynik zgadywania
//                }

//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Błąd: {ex.Message}\n");
//                }
//            }
//        }

//        // Funkcja do wyświetlania wyniku zgadywania w czytelny sposób
//        static void DisplayResult(GuessResult result)
//        {
//            // Wyświetlamy wynik zgadywania za pomocą symboli i ustawiamy odpowiedni kolor tła konsoli
//            Console.WriteLine("Wynik:\n");
//            Console.ForegroundColor = ConsoleColor.DarkRed;

//            // Wyświetlamy idealne trafienia i trafienia na złej pozycji
//            for (int i = 0; i < result.ExactMatches; i++)
//            {
//                Console.Write("* ");     // idealne trafienia
//            }

//            // ustawiamy kolor dla trafień na złej pozycji i wyświetlamy je 
//            Console.ForegroundColor = ConsoleColor.White;
//            for (int i = 0; i < result.PartialMatches; i++)
//            {
//                Console.Write("o ");     // trafienia na złej pozycji
//            }

//            // Resetujemy kolor konsoli do domyślnego
//            Console.ResetColor();

//            // Wyświetlamy dokładne liczby trafień i trafień na złej pozycji`
//            Console.WriteLine($" (Dokładne: {result.ExactMatches}, Zła pozycja: {result.PartialMatches})\n");
//        }
//    }
//}


// =-=-=-=-=-=--=-=-=-= ZADANIE 2 =-=-=-=-=-=--=-=-=-= 

// Piotr Bacior 15 722 

//using System;
//using System.Linq.Expressions;
//using MasterMind.Engine;

//namespace MasterMind.CLI
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // Wyświetlamy menu gry Master Mind
//            Console.Clear();
//            Console.WriteLine("-_-_-_-_-_ GRA MASTER MIND -_-_-_-_-_\n");
//            Console.WriteLine("Autor: Piotr Bacior 15 722\n");
//            Console.WriteLine("1. Zgaduj kod (Człowiek VS Komputer)");
//            Console.WriteLine("2. Pomyśl kod (Komputer VS Człowiek)");
//            Console.WriteLine("3. Wyjście\n");
//            Console.Write("Wybierz opcję: ");

//            // Definiujemy zmienną do przechowywania wyboru użytkownika
//            var key = Console.ReadKey();
//            Console.WriteLine();

//            // Obsługujemy wybór użytkownika za pomocą instrukcji switch
//            if (key.Key == ConsoleKey.D1)
//            {
//                PlayHumanGuesser();                 // Funkcja do obsługi trybu, w którym człowiek zgaduje kod
//            }
//            else if (key.Key == ConsoleKey.D2)
//            {
//                PlayComputerGuesser();              // Funkcja do obsługi trybu, w którym komputer zgaduje kod
//            }
//            else if (key.Key == ConsoleKey.D3)
//            {
//                return;                               // Wyjście z programu
//            }

//        }

//        // Definiujemy funkcję do obsługi trybu, w którym człowiek zgaduje kod
//        static void PlayHumanGuesser()
//        {
//            Game game = new Game();         // tworzymy nową grę MasterMind

//            Console.WriteLine("\n[TRYB] Zgadujesz kod komputera.");
//            Console.WriteLine($"Dostępne kolory: {game.GetAllowedColors()}");

//            // Główna pętla gry - kontynuujemy aż do zakończenia gry
//            while (!game.isGameOver)
//            {
//                // Pobieramy dane wejściowe od użytkownika 
//                Console.WriteLine($"Próba {game.AttemptsUsed + 1}: ");

//                // Tworzymy zmienną input do przechowywania danych wejściowych użytkownika
//                string input = Console.ReadLine()?.Trim().ToLower();

//                // Walidacja danych wejściowych - sprawdzamy czy wprowadzono dokładnie 4 litery
//                if (string.IsNullOrEmpty(input) || input.Length != 4)
//                {
//                    Console.WriteLine("Błąd: Wpisz 4 znaki.");
//                    continue;
//                }

//                // Funkcja do wyświetlania wyniku zgadywania
//                try
//                {
//                    var result = game.EvaluateGuess(input);         // oceniamy zgadywanie użytkownika
//                    DisplayResult(result);                          // wyświetlamy wynik zgadywania
//                }

//                // Obsługujemy wyjąki i wyświetlamy komunikaty o błędach
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Błąd: {ex.Message}\n");
//                }

//            }

//            // Wyświetlamy komunikat o zakończeniu gry
//            if (game.isGameWon)
//            {
//                Console.WriteLine("Gratulacje! Odgadłeś kod!");
//            }

//            // Wyświetlamy prawidłowy kod jeżeli użytkownik nie odgadł kodu
//            else
//            {
//                Console.WriteLine($"Koniec gry! Prawidłowy kod to: {game.RevealCode()}");
//            }

//            // Czekamy na naciśnięcie klawisza przed zakończeniem gry, a następnie wracamy do menu głównego
//            Console.WriteLine("Naciśnij dowolny klawisz...");
//            Console.ReadKey();
//        }

//        // Definiujemy funkcję do obsługi trybu, w którym komputer zgaduje kod
//        static void PlayComputerGuesser()
//        {
//            // Definiujemy zmienną solver jako nowy obiekt ComputerSolver
//            ComputerSolver solver = new ComputerSolver();
//            Console.WriteLine("\n[TRYB] Pomyśl sekretny kod (4 znaki: r, y, g, b, m, c)");
//            Console.WriteLine("Zapisz go sobie na kartce, nie wpisuj go tutaj!");
//            Console.WriteLine("Naciśnij ENTER, gdy będziesz gotowy...");
//            Console.ReadLine();

//            bool solved = false;    // Flaga do kontrolowania pętli zgadywania komputera

//            // Główna pętla zgadywania komputera - kontynuujemy aż do odgadnięcia kodu
//            while (!solved)
//            {
//                try
//                {
//                    string guess = solver.GetNextGuess();    // Pobieramy następną propozycję komputera

//                    // Wyświetlamy propozycję komputera i liczbę pozostałych możliwych kombinacji
//                    Console.ForegroundColor = ConsoleColor.DarkCyan;
//                    Console.WriteLine($"\nKomputer zgaduje: {guess.ToUpper()}");
//                    Console.ResetColor();
//                    Console.WriteLine($"Możliwych kombinacji pozostało: {solver.ReminingPossibilities}");

//                    Console.Write("Podaj liczbę trafień DOKŁADNYCH (czarne - właściwa pozycja).\n");

//                    // Pobieramy liczbę trafień dokładnych od użytkownika
//                    int exact = int.Parse(Console.ReadLine());

//                    // Sprawdzamy czy komputer odgadł kod 
//                    if (exact == 4)
//                    {
//                        Console.WriteLine($"\nKomputer odgadł Twój kod w {solver.MoveCount} ruchach!");

//                        solved = true;  // Ustawiamy flagę na true, aby zakończyć pętlę
//                        break;
//                    }

//                    Console.WriteLine("Podaj liczbę trafień NIEDOKŁADNYCH (białe - zła pozycja).\n");

//                    // Pobieramy liczbę trafień niedokładnych od użytkownika
//                    int partial = int.Parse(Console.ReadLine());

//                    // Walidujemy sumę trafień dokładnych i niedokładnych
//                    if (exact + partial > 4)
//                    {
//                        Console.WriteLine("Błąd: Suma trafień dokładnych i niedokładnych nie może przekraczać 4. Spróbuj ponownie.");

//                        solver.Reset();                        // Resetujemy stan solvera

//                        continue;                              // Przechodzimy do następnej iteracji pętli
//                    }
//                    solver.ProcessFeedback(exact, partial);    // Przetwarzamy informacje zwrotne od użytkownika
//                }

//                // Obsługujemy wyjątki i wyświetlamy komunikaty o błędach
//                catch (InvalidOperationException ex)
//                {
//                    // Specjalny komunikat w przypadku wykrycia oszustwa przez użytkownika
//                    Console.ForegroundColor = ConsoleColor.Red;
//                    Console.WriteLine($"\nUWAGA!: {ex.Message}");
//                    Console.WriteLine("Twoje odpowiedzi były sprzeczne. Komputer nie może znaleźć kodu.");
//                    Console.ResetColor();
//                    break;
//                }

//                // Obsługujemy inne wyjątki i wyświetlamy komunikaty o błędach
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Błąd: {ex.Message}\n");
//                }

//                // Czekamy na naciśnięcie klawisza przed kolejnym zgadywaniem i odczytujemy go
//                Console.WriteLine("Naciśnij dowolny klawisz");
//                Console.ReadKey();
//            }   
//        }
//        // Funkcja do wyświetlania wyniku zgadywania w czytelny sposób
//        static void DisplayResult(GuessResult result)
//        {
//            // Wyświetlamy wynik zgadywania za pomocą symboli i ustawiamy odpowiedni kolor tła konsoli
//            Console.ForegroundColor = ConsoleColor.DarkRed;

//            // Wyświetlamy idealne trafienia i trafienia na złej pozycji
//            for (int i = 0; i < result.ExactMatches; i++)
//            {
//                Console.Write("* ");     // idealne trafienia
//            }

//            // ustawiamy kolor dla trafień na złej pozycji i wyświetlamy je
//            Console.ForegroundColor = ConsoleColor.White;

//            // Wyświetlamy trafienia na złej pozycji
//            for (int i = 0; i < result.PartialMatches; i++)
//            {
//                Console.Write("o ");
//            }

//            // Resetujemy kolor konsoli do domyślnego
//            Console.ResetColor();

//            // Wyświetlamy dokładne liczby trafień i trafień na złej pozycji
//            Console.WriteLine($"({result.ExactMatches}, {result.PartialMatches})");
//        }
//    }
//}


// =-=-=-=-=-=--=-=-=-= ZADANIE 3 =-=-=-=-=-=--=-=-=-= 

// Piotr Bacior 15 722

using System;
using MasterMind.Engine;

namespace MasterMind.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

    }
}
