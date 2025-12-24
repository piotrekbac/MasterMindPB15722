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
using System.Runtime.ExceptionServices;
using MasterMind.Engine;

namespace MasterMind.CLI
{
    class Program
    {
        // Ustawiamy domyślne wartości N i K
        static int currentN = 6;
        static int currentK = 4;

        static void Main(string[] args)
        {
            while (true)
            {
                // Wyświetlamy menu gry Master Mind
                Console.Clear();
                Console.WriteLine("-_-_-_-_-_ GRA MASTER MIND -_-_-_-_-_\n");
                Console.WriteLine("Autor: Piotr Bacior 15 722\n");
                Console.WriteLine("1. Nowa gra (Człowiek VS Komputer)");
                Console.WriteLine("2. Nowa gra (Komputer vs Człowiek) - Przemyśl swój kod");
                Console.WriteLine("3. Ustawienia gry (N i K)");
                Console.WriteLine("4. Wyjście\n");
                Console.Write("Wybierz opcję");

                // Definiujemy zmienną do przechowywania wyboru użytkownika
                var key = Console.ReadKey();
                Console.WriteLine();

                // Obsługujemy wybór użytkownika za pomocą instrukcji switch
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        PlayHumanGuesser();         // Funkcja do obsługi trybu, w którym człowiek zgaduje kod
                        break;
                    case ConsoleKey.D2:
                        PlayComputerGuesser();      // Funkcja do obsługi trybu, w którym komputer zgaduje kod
                        break;
                    case ConsoleKey.D3:
                        ConfigureGameParameters();                                // Funkcja do obsługi menu ustawień gry
                        break;
                    case ConsoleKey.D4:
                        return;                                         // Wyjście z programu
                }
            }
        }

        // Definiuejmy funkcję do obsługi menu ustawień gry
        static void ConfigureGameParameters()
        {
            Console.WriteLine("\n =-=-=-=- KONFIGURACJA -=-=-=-=-= ");
            Console.WriteLine("Wymagania: 6 <= n <= 8, 4 <= k <= 6, k < n");

            // Definiujemy zmienne do przechowywania nowych wartości N i K
            int newN = 0;

            // Pętla do pobierania i walidacji wartości N
            while (true)
            {
                Console.WriteLine("Podaj liczbę kolorów (n) [6-8]");

                // Pobieramy i walidujemy wartość N od użytkownika
                if (int.TryParse(Console.ReadLine(), out newN) && newN >= 6 && newN <= 8) break;

                Console.WriteLine("Błąd. Podaj liczbę 6,7 lub 8.");
            }

            // Definiujemy zmienną do przechowywania nowej wartości K
            int newK = 0;

            // Pętla do pobierania i walidacji wartości K
            while (true)
            {
                Console.WriteLine($"Podaj długość kodów (k) [4-6] (musi być < {newN}): ");

                // Pobieramy i walidujemy wartość K od użytkownika
                if (int.TryParse(Console.ReadLine(), out newK) && newK >= 4 && newK <= 6)
                {
                    // Sprawdzamy czy K jest mniejsze od N
                    if (newK < newN) break;

                    // Wyświetlamy komunikat o błędzie jeżeli K nie jest mniejsze od N
                    else
                    {
                        Console.WriteLine($"Błąd. Długość kodu musi być mniejsza od liczby kolorów ({newK}). ");
                    }
                }

                // Wyświetlamy komunikat o błędzie jeżeli wartość K jest nieprawidłowa
                else
                {
                    Console.WriteLine("Błąd. Podaj liczbę 4, 5 lub 6.");
                }
            }

            currentN = newN;    // Aktualizujemy wartość N
            currentK = newK;    // Aktualizujemy wartość K
            Console.WriteLine("Ustawienia zapisane! Naciśnij klawisz...");
            Console.ReadKey();  // Czekamy na naciśnięcie klawisza przed powrotem do menu głównego
        }

        // Definiujemy funkcję do obsługi menu ustawień gry
        static void PlayHumanGuesser()
        {
            // Tworzymy nową grę MasterMind z aktualnymi wartościami N i K
            Game game = new Game(currentN, currentK);

            Console.WriteLine($"\n[TRYB] Zgadujesz kod ({currentK} znaki z {currentN} kolorów).");
            Console.WriteLine($"Dostępne kolory: {game.GetAllowedColors()}");

            // Główna pętla gry - kontynuujemy aż do zakończenia gry
            while (!game.isGameOver)
            {
                Console.Write($"Próba {game.AttemptsUsed + 1}");

                // Tworzymy zmienną input do przechowywania danych wejściowych użytkownika
                string input = Console.ReadLine()?.Trim().ToLower();

                // Walidacja danych wejściowych - sprawdzamy czy wprowadzono dokładnie K liter
                if (string.IsNullOrEmpty(input) || input.Length != currentK)
                {
                    Console.WriteLine($"Błąd: Wpisz dokładnie {currentK} znaki.");
                    continue;
                }

                // Funkcja do wyświetlania wyniku zgadywania
                try
                {
                    var result = game.EvaluateGuess(input);         // oceniamy zgadywanie użytkownika
                    DisplayResult(result);                          // wyświetlamy wynik zgadywania
                }

                // Obsługujemy wyjątki i wyświetlamy komunikaty o wszelkich błędach. 
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}\n");
                }
            }

            // Wyświetlamy komunikat o zakończeniu gry
            if (game.isGameWon)
            {
                Console.WriteLine("Gratulacje! Odgadłeś kod!");
            }

            // Wyświetlamy prawidłowy kod jeżeli użytkownik nie odgadł kodu
            else
            {
                Console.WriteLine($"Koniec gry! Prawidłowy kod to: {game.RevealCode()}");
            }

            // Czekamy na naciśnięcie klawisza przed zakończeniem gry, a następnie wracamy do menu głównego
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        // Definiujemy funkcję do obsługi trybu, w którym komputer zgaduje kod
        static void PlayComputerGuesser()
        {
            // Definiujemy zmienną solver jako nowy obiekt ComputerSolver z aktualnymi wartościami N i K
            ComputerSolver solver = new ComputerSolver(currentN, currentK);

            Console.WriteLine($"\n[TRYB] Pomyśl kod ({currentK} znaki z puli: {new Game(currentN, currentK).GetAllowedColors()})");
            Console.WriteLine("Naciśnij ENTER, gdy będziesz gotowy przejsć dalej...");
            Console.ReadLine();

            bool solved = false;    // Flaga do kontrolowania pętli zgadywania komputera

            // Główna pętla zgadywania komputera - kontynuujemy aż do odgadnięcia kodu
            while (!solved)
            {
                // Sprawdzamy czy komputer odgadł kod
                try
                {
                    string guess = solver.GetNextGuess();    // Pobieramy następną propozycję komputera

                    // Wyświetlamy propozycję komputera i liczbę pozostałych możliwych kombinacji
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"\nKomputer: {guess.ToUpper()} (Możliwych: {solver.ReminingPossibilities})");
                    Console.ResetColor();

                    // Pobieramy liczbę trafień dokładnych od użytkownika
                    Console.WriteLine("Trafienia DOKŁADNE (czarne)");
                    int exact = int.Parse(Console.ReadLine());              // Liczba trafień dokładnych 

                    // Sprawdzamy czy komputer odgadł kod
                    if (exact == currentK)
                    {
                        Console.WriteLine($"\nKomputer odgadł Twój kod w {solver.MoveCount} ruchach!");
                        solved = true;  // Ustawiamy flagę na true, aby zakończyć pętlę
                        break;
                    }

                    Console.WriteLine("Trafienia NIEDOKŁADNE (białe)");
                    int partial = int.Parse(Console.ReadLine());            // Liczba trafień niedokładnych

                    // Walidujemy sumę trafień dokładnych i niedokładnych
                    if (exact + partial > currentK)
                    {
                        Console.WriteLine($"Błąd: Suma trafień > {currentK}. Spróbuj ponowanie.");
                        continue;
                    }

                    solver.ProcessFeedback(exact, partial);    // Przetwarzamy informacje zwrotne od użytkownika
                }

                // Wyłapujemy wyjątki i wyświetlamy komunikaty o błędach
                catch (InvalidOperationException ex)
                {
                    // Specjalny komunikat w przypadku wykrycia oszustwa przez użytkownika
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nOSZUSTWO: {ex.Message}");
                    Console.ResetColor();
                    break;
                }

                // Wyłapujemy inne wyjątki i wyświetlamy komunikaty o błędach
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}\n");
                }
            }
            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować... ");
            Console.ReadKey(); // Czekamy na naciśnięcie klawisza przed powrotem do menu głównego 

        }

        // Funkcja do obsługi menu ustawień gry
        static void DisplayResult(GuessResult result)
        {
            // Wyświetlamy wynik zgadywania za pomocą symboli i ustawiamy odpowiedni kolor tła konsoli
            Console.ForegroundColor = ConsoleColor.DarkRed;

            // Wyświetlamy idealne trafienia
            for (int i = 0; i < result.ExactMatches; i++)
            {
                Console.WriteLine("* ");
            }

            // ustawiamy kolor dla trafień na złej pozycji i wyświetlamy je
            Console.ForegroundColor = ConsoleColor.White;

            // Wyświetlamy trafienia na złej pozycji
            for (int i = 0; i < result.PartialMatches; i++)
            {
                Console.WriteLine("o ");
            }

            // Resetujemy kolor konsoli do domyślnego
            Console.ResetColor();

            // Wyświetlamy dokładne liczby trafień i trafień na złej pozycji
            Console.WriteLine($"({result.ExactMatches}, {result.PartialMatches})");
        }
    }
}
