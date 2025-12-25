using System; 
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
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
        //public bool isVictory => ExactMatches == 4;     // zakładamy, 4 kolory do odgadnięcia dla standardowej gry

        // na poczet zadania 5 - logika zwycięstwa jest w klasie Game
        public bool isVictory => false;     // Domyślnie ustawiamy na false, rzeczywista wartość powinna być ustalana w kontekście gry

        // Konstruktor do inicjalizacji wyniku zgadywania - dokładne i niedokładne trafienia
        public GuessResult(int exact, int partial)
        {
            ExactMatches = exact;
            PartialMatches = partial;
        }

        // Dodajemy operator sprawdzenia równości
        public static bool operator ==(GuessResult a, GuessResult b)
        {
            // Zwracamy true, jeśli zarówno ExactMatches, jak i PartialMatches są równe
            return a.ExactMatches == b.ExactMatches && a.PartialMatches == b.PartialMatches;
        }

        // Dodajemy operator sprawdzenia nierówności 
        public static bool operator !=(GuessResult a, GuessResult b)
        {
            // Zwracamy negację operatora równości
            return !(a == b);
        }

        // Nadpisujemy metody Equals poprawnego porównywania struktur
        public override bool Equals(object obj)
        {
            // Sprawdzamy, czy obiekt jest typu GuessResult i porównujemy wartości
            if (obj is GuessResult other) return this == other;

            // Jeśli obiekt nie jest typu GuessResult, zwracamy false
            return false;
        }

        // Nadpisujemy metodę GetHashCode dla poprawnego działania w strukturach danych
        public override int GetHashCode()
        {
            // Generujemy hash code na podstawie ExactMatches i PartialMatches
            return (ExactMatches, PartialMatches).GetHashCode();
        }
    }

    // Tworzymy klasę reprezentującą logikę gry MasterMind 
    //public class Game
    //{
    //    // Konfiguracja gry - dozwolone kolory, długość zgadywanych pozycji, maksymalna liczba prób
    //    private readonly char[] _allowedColors;
    //    private readonly int _codeLength;
    //    private readonly int _maxAttempts;

    //    // Stan naszej gry
    //    private char[] _secretCode;                         // kod do odgadnięcia (w znaczeniu pozycji do odgadnięcia)
    //    public int AttemptsUsed { get; private set; }       // liczba wykorzystanych prób
    //    public bool isGameOver { get; private set; }        // czy gra się zakończyła
    //    public bool isGameWon { get; private set; }         // czy gra została wygrana


    //    // Historia gry - przechowujemy zgadywania i ich wyniki w formie ala snapshot'u stanu gry
    //    public List<(string Guess, GuessResult Result)> History { get; private set; } // historia zgadywań i ich wyników

    //    // Konstruktor inicjalizujący grę z domyślnymi parametrami - 6 kolorów, długość kodu do zgadnięcia 4, maksymalnie 9 prób
    //    public Game(int codeLength = 4, int maxAttempts = 9)
    //    {
    //        _allowedColors = new char[] { 'r', 'y', 'g', 'b', 'm', 'c' };   // red, yellow, green, blue, magenta, cyan
    //        _codeLength = codeLength;
    //        _maxAttempts = maxAttempts;
    //        History = new List<(string, GuessResult)>();
    //        StartNewGame();  // rozpoczynamy nową grę przy tworzeniu instancji
    //    }

    //    // Metoda rozpoczynająca nową grę - generuje losowy kod do odgadnięcia i resetuje stan gry
    //    public void StartNewGame()
    //    {
    //        AttemptsUsed = 0;                       // ustawiamy liczbę wykorzystanych prób na 0
    //        isGameOver = false;                     // resetujemy stan zakończenia gry
    //        isGameWon = false;                      // resetujemy stan wygranej
    //        History.Clear();                        // czyścimy historię zgadywań
    //        _secretCode = GenerateSecretCode();     // generujemy nowy kod do odgadnięcia
    //    }

    //    // Metoda do przetwarzania zgadywania gracza - generowanie losowego kodu do odgadnięcia (wariacja z powtórzeniami)
    //    private char[] GenerateSecretCode()
    //    {
    //        // Generujemy losowy kod z dozwolonych kolorów
    //        Random rnd = new Random();

    //        // Tworzymy tablicę znaków reprezentującą kod do odgadnięcia
    //        char[] code = new char[_codeLength];

    //        // Losujemy kolory dla każdej pozycji w kodzie
    //        for (int i = 0; i < code.Length; i++)
    //        {
    //            // Wybieramy losowy kolor z dozwolonych kolorów
    //            code[i] = _allowedColors[rnd.Next(_allowedColors.Length)];
    //        }

    //        // Zwracamy wygenerowany kod
    //        return code;
    //    }

    // =-=-=-=-=-=--=-=-=-= ZADANIE 1 =-=-=-=-=-=--=-=-=-= 

    //// Metoda oceniająca zgadywanie gracza i zwracająca wynik (trafienia dokładne i niedokładne)
    //public GuessResult EvaluateGuess(string guessInput)
    //{
    //    // Sprawdzamy poprawność zgadywania - czy gra się zakończyła oraz czy długość zgadywania jest poprawna
    //    if (isGameOver) throw new InvalidOperationException("Gra się zakończyła. Rozpocznij nową grę.");
    //    if (guessInput.Length != _codeLength) throw new ArgumentException($"Zgadywana długość kodu musi mieć długość {_codeLength}.");

    //    // Konwertujemy zgadywanie na tablicę znaków dla łatwiejszej manipulacji
    //    char[] guess = guessInput.ToLower().ToCharArray();

    //    // Inicjalizujemy liczniki trafień dokładnych i niedokładnych
    //    int exactMatches = 0;
    //    bool[] secretMatched = new bool[_codeLength];
    //    bool[] guessMatched = new bool[_codeLength];

    //    // Najpierw sprawdzamy trafienia dokładne (kolory na właściwych pozycjach)
    //    for (int i = 0; i < _codeLength; i++)
    //    {
    //        // Sprawdzamy, czy kolor na pozycji i jest trafiony dokładnie
    //        if (guess[i] == _secretCode[i])
    //        {
    //            exactMatches++;                 // Zwiększamy licznik trafień dokładnych
    //            secretMatched[i] = true;        // Ta pozycja w kodzie sekretnym jest już "zużyta"
    //            guessMatched[i] = true;         // Ta pozycja w próbie jest już "zużyta"
    //        }
    //    }

    //    // Następnie sprawdzamy trafienia niedokładne (kolory na niewłaściwych pozycjach)
    //    int partialMatches = 0;

    //    // Szukamy tego koloru w sekretnym kodzie, pomijając już trafione dokładnie pozycje 
    //    for (int i = 0; i < _codeLength; i++)
    //    {
    //        // Pomijamy już trafione dokładnie pozycje
    //        if (guessMatched[i]) continue;

    //        // Szukamy koloru z próby w kodzie sekretnym
    //        for (int j = 0; j < _codeLength; j++)
    //        {
    //            // Pomijamy już trafione dokładnie pozycje w kodzie sekretnym
    //            if (secretMatched[j]) continue;

    //            // Sprawdzamy, czy kolor z próby pasuje do koloru w kodzie sekretnym
    //            if (guess[i] == _secretCode[j])
    //            {
    //                partialMatches++;              // Zwiększamy licznik trafień niedokładnych
    //                secretMatched[j] = true;       // Zużywany pozycję w kodzie sekretnym
    //                break;                         // Przechodzimy do następnej pozycji w zgadywaniu
    //            }
    //        }
    //    }


    //    AttemptsUsed++;                                                // Zwiększamy liczbę wykorzystanych prób
    //    var result = new GuessResult(exactMatches, partialMatches);    // Tworzymy wynik zgadywania

    //    History.Add((guessInput, result));                             // Dodajemy zgadywanie i jego wynik do historii


    //    // Sprawdzamy, czy gra się zakończyła (wygrana lub wyczerpane próby)
    //    if (result.isVictory)
    //    {
    //        isGameOver = true;    // Gra zakończona wygraną
    //        isGameWon = true;     // Ustawiamy stan wygranej
    //    }
    //    else if (AttemptsUsed >= _maxAttempts)
    //    {
    //        isGameWon = false;    // Ustawiamy stan przegranej
    //        isGameOver = true;    // Gra zakończona przegraną (wyczerpane próby)
    //    }

    //    return result;  // Zwracamy wynik zgadywania
    //}

    //// Metoda zwracająca dozwolone kolory jako string do wyświetlenia graczowi
    //public string GetAllowedColors()
    //{
    //    return string.Join(", ", _allowedColors);
    //}

    //// Metoda ujawniająca kod do odgadnięcia po zakończeniu gry
    //public string RevealSecretCode()
    //{
    //    if (!isGameOver) return "GRA W TOKU!";      // Nie ujawniamy kodu, jeśli gra jeszcze trwa
    //    return new string(_secretCode);             // Ujawnienie kodu po zakończeniu gry
    //}

    //// Metoda ujawniająca kod do odgadnięcia (do celów debugowania) - jest to opcja tylko dla debugowania gry
    //public string DebugGetCode()
    //{
    //    return new string(_secretCode);             // Ujawnienie kodu po zakończeniu gry
    //}


    //=-=-=-=-=-=--=-=-=-= ZADANIE 2 =-=-=-=-=-=--=-=-=-= 

    // Statyczna metoda do obliczania wyniku zgadywania (trafienia dokładne i niedokładne) bez potrzeby tworzenia instancji gry
    //public static GuessResult CalculateScore(string secretCodeStr, string guessStr)
    //{
    //    char[] secret = secretCodeStr.ToCharArray();        // konwertujemy kod sekretny na tablicę znaków
    //    char[] guess = guessStr.ToCharArray();              // konwertujemy zgadywanie na tablicę znaków
    //    int len = secret.Length;                            // długość kodu (zakładamy, że oba mają tę samą długość)

    //    // Inicjalizujemy liczniki trafień dokładnych i niedokładnych
    //    int exactMatches = 0;
    //    bool[] secretMatched = new bool[len];               // tablica do śledzenia trafionych pozycji w kodzie sekretnym
    //    bool[] guessMatched = new bool[len];                // tablica do śledzenia trafionych pozycji w zgadywaniu

    //    // Najpierw sprawdzamy trafienia dokładne (kolory na właściwych pozycjach)
    //    for (int i = 0; i < len; i++)
    //    {
    //        // Sprawdzamy czy kolor na pozycji i jest trafiony dokładnie
    //        if (guess[i] == secret[i])
    //        {
    //            exactMatches++;                             // Zwiększamy licznik trafień dokładnych
    //            secretMatched[i] = true;                    // Ta pozycja w kodzie sekretnym jest już "zużyta"
    //            guessMatched[i] = true;                     // Ta pozycja w próbie jest już "zużyta"
    //        }
    //    }

    //    int partialMatches = 0;                             // Inicjalizujemy licznik trafień niedokładnych
    //    for (int i = 0; i < len; i++)
    //    {
    //        if (guessMatched[i]) continue;                  // Pomijamy już trafione dokładnie pozycje

    //        // Szukamy koloru z próby w kodzie sekretnym
    //        for (int j = 0; j < len; j++)
    //        {
    //            if (!secretMatched[j] && guess[i] == secret[j])
    //            {
    //                partialMatches++;                       // Zwiększamy licznik trafień niedokładnych
    //                secretMatched[j] = true;                // Zużywamy pozycję w kodzie sekretnym
    //                break;                                  // Przechodzimy do następnej pozycji w zgadywaniu
    //            }
    //        }
    //    }

    //    return new GuessResult(exactMatches, partialMatches);       // Zwracamy wynik zgadywania
    //}

    //// Metoda oceniająca zgadywanie gracza i zwracająca wynik (trafienia dokładne i niedokładne)
    //public GuessResult EvaluateGuess(string guessInput)
    //{
    //    // Sprawdzamy poprawność zgadywania - czy gra się zakończyła oraz czy długość zgadywania jest poprawna
    //    if (isGameOver) throw new InvalidOperationException("Gra się zakończyła. Rozpocznij nową grę.");

    //    // Sprawdzamy długość zgadywania
    //    if (guessInput.Length != _codeLength) throw new ArgumentException($"Zgadywana długość kodu musi mieć długość {_codeLength}.");

    //    // Konwertujemy zgadywanie na tablicę znaków dla łatwiejszej manipulacji
    //    var result = CalculateScore(new string(_secretCode), guessInput); // Obliczamy wynik zgadywania


    //    AttemptsUsed++;                                                // Zwiększamy liczbę wykorzystanych prób
    //    History.Add((guessInput, result));                             // Dodajemy zgadywanie i jego wynik do historii

    //    // Sprawdzamy, czy gra się zakończyła (wygrana)
    //    if (result.isVictory)
    //    {
    //        isGameOver = true;    // Gra zakończona wygraną
    //        isGameWon = true;     // Ustawiamy stan wygranej
    //    }

    //    // Sprawdzamy, czy gra się zakończyła (wyczerpane próby)
    //    else if (AttemptsUsed >= _maxAttempts)
    //    {
    //        isGameWon = false;    // Ustawiamy stan przegranej
    //        isGameOver = true;    // Gra zakończona przegraną (wyczerpane próby)
    //    }

    //    return result;  // Zwracamy wynik zgadywania
    //}

    //// Metoda ujawniająca kod do odgadnięcia po zakończeniu gry
    //public string GetAllowedColors() => string.Join(", ", _allowedColors);


    //// Metoda ujawniająca kod do odgadnięcia po zakończeniu gry
    //public string RevealCode() => isGameOver? new string(_secretCode) : "GRA W TOKU!";

    //=-=-=-=-=-=--=-=-=-= ZADANIE 3 =-=-=-=-=-=--=-=-=-= 

    //    // Tutaj dodajemy dodatkowe funkcje lub klasy zgodnie z wymaganiami zadania 3
    //    public class Game
    //    {
    //        // Konstruktor inicjalizujący grę z określoną długością kodu i dozwolonymi kolorami
    //        private static readonly char[] _allAvailableColors = { 'r', 'y', 'g', 'b', 'm', 'c' };

    //        // Konstruktor inicjalizujący grę z określoną długością kodu i dozwolonymi kolorami
    //        private readonly char[] _currentAllowedColors;

    //        // Długość kodu do odgadnięcia
    //        public int CodeLength { get; private set; }

    //        // Maksymalna liczba prób
    //        private readonly int _maxAttempts;

    //        private char[] _secretCode;                         // kod do odgadnięcia (w znaczeniu pozycji do odgadnięcia)
    //        public int AttemptsUsed { get; private set; }       // liczba wykorzystanych prób
    //        public bool isGameOver { get; private set; }        // czy gra się zakończyła
    //        public bool isGameWon { get; private set; }         // czy gra została wygrana
    //        public List<(string Guess, GuessResult Result)> History { get; private set; } // historia zgadywań i ich wyników

    //        public Game(int n = 6, int k = 4, int maxAttempts = 10)
    //        {
    //            // Walidujemy parametry n i k
    //            if (n < 6 || n > 8) throw new ArgumentException("Liczba kolorów (n) musi wynosić 6,7 lub 8.");
    //            if (k < 4 || k > 6) throw new ArgumentException("Długość kodu (k) musi wynosić 4, 5 lub 6.");
    //            if (k >= n) throw new ArgumentException("Długość kodu (k) musi być mniejsza od liczby kolorów (n).");

    //            // Ustawiamy dozwolone kolory na podstawie wartości n
    //            _currentAllowedColors = _allAvailableColors.Take(n).ToArray();
    //            CodeLength = k;                 // Ustawiamy długość kodu do odgadnięcia
    //            _maxAttempts = maxAttempts;     // Ustawiamy maksymalną liczbę prób

    //            // Inicjalizujemy historię zgadywań
    //            History = new List<(string, GuessResult)>();

    //            // Rozpoczynamy nową grę
    //            StartNewGame();
    //        }

    //        // Metoda rozpoczynająca nową grę - generuje losowy kod do odgadnięcia i resetuje stan gry
    //        public void StartNewGame()
    //        {
    //            AttemptsUsed = 0;                       // ustawiamy liczbę wykorzystanych prób na 0
    //            isGameOver = false;                     // resetujemy stan zakończenia gry
    //            isGameWon = false;                      // resetujemy stan wygranej
    //            History.Clear();                        // czyścimy historię zgadywań
    //            _secretCode = GenerateSecretCode();     // generujemy nowy kod do odgadnięcia
    //        }

    //        // Metoda do przetwarzania zgadywania gracza - generowanie losowego kodu do odgadnięcia (wariacja bez powtórzeń)
    //        private char[] GenerateSecretCode()
    //        {
    //            // Generujemy losowy kod z dozwolonych kolorów bez powtórzeń
    //            Random rnd = new Random();
    //            char[] code = new char[CodeLength];

    //            // Losujemy kolory dla każdej pozycji w kodzie bez powtórzeń
    //            for (int i = 0; i < CodeLength; i++)
    //            {
    //                code[i] = _currentAllowedColors[rnd.Next(_currentAllowedColors.Length)];
    //            }

    //            // Zwracamy wygenerowany kod
    //            return code;
    //        }

    //        // Statyczna metoda do obliczania wyniku zgadywania (trafienia dokładne i niedokładne) bez potrzeby tworzenia instancji gry
    //        public static GuessResult CalculateScore(string secretCodeStr, string guessStr)
    //        {
    //            char[] secret = secretCodeStr.ToCharArray();        // konwertujemy kod sekretny na tablicę znaków
    //            char[] guess = guessStr.ToCharArray();              // konwertujemy zgadywanie na tablicę znaków
    //            int len = secret.Length;                            // długość kodu (zakładamy, że oba mają tę samą długość)

    //            int exactMatches = 0;
    //            bool[] secretMatched = new bool[len];               // tablica do śledzenia trafionych pozycji w kodzie sekretnym
    //            bool[] guessMatched = new bool[len];                // tablica do śledzenia trafionych pozycji w zgadywaniu


    //            // Najpierw sprawdzamy trafienia dokładne (kolory na właściwych pozycjach)
    //            for (int i = 0; i < len; i++)
    //            {
    //                // Sprawdzamy czy kolor na pozycji i jest trafiony dokładnie
    //                if (guess[i] == secret[i])
    //                {
    //                    exactMatches++;                    // Zwiększamy licznik trafień dokładnych
    //                    secretMatched[i] = true;           // Ta pozycja w kodzie sekretnym jest już "zużyta"
    //                    guessMatched[i] = true;            // Ta pozycja w próbie jest już "zużyta"
    //                }
    //            }


    //            int partialMatches = 0;                             // Inicjalizujemy licznik trafień niedokładnych

    //            // Szukamy tego koloru w sekretnym kodzie, pomijając już trafione dokładnie pozycje
    //            for (int i = 0; i < len; i++)
    //            {
    //                if (guessMatched[i]) continue;                  // Pomijamy już trafione dokładnie pozycje

    //                // Szukamy koloru z próby w kodzie sekretnym
    //                for (int j = 0; j < len; j++)
    //                {
    //                    if (!secretMatched[j] && guess[i] == secret[j])
    //                    {
    //                        partialMatches++;                       // Zwiększamy licznik trafień niedokładnych
    //                        secretMatched[j] = true;                // Zużywamy pozycję w kodzie sekretnym
    //                        break;                                  // Przechodzimy do następnej pozycji w zgadywaniu
    //                    }
    //                }
    //            }

    //            return new GuessResult(exactMatches, partialMatches);       // Zwracamy wynik zgadywania
    //        }

    //        // Definicja metody oceniającej zgadywanie gracza i zwracającej wynik (trafienia dokładne i niedokładne)
    //        public GuessResult EvaluateGuess(string guessInput)
    //        {
    //            // Sprawdzamy poprawność zgadywania - czy gra się zakończyła oraz czy długość zgadywania jest poprawna
    //            if (isGameOver) throw new InvalidOperationException("Gra się zakończyła. Rozpocznij nową grę.");

    //            // Sprawdzamy długość zgadywania
    //            if (guessInput.Length != CodeLength) throw new ArgumentException($"Zgadywana długość kodu musi mieć długość {CodeLength}.");

    //            // Obliczamy wynik zgadywania i przypisujemy go do zmiennej result
    //            var result = CalculateScore(new string(_secretCode), guessInput); 


    //            AttemptsUsed++;                                                // Zwiększamy liczbę wykorzystanych prób
    //            History.Add((guessInput, result));                             // Dodajemy zgadywanie i jego wynik do historii


    //            // Sprawdzamy, czy gra się zakończyła (wygrana)
    //            if (result.ExactMatches == CodeLength)
    //            {
    //                isGameWon = true;     // Ustawiamy stan wygranej
    //                isGameOver = true;    // Gra zakończona wygraną
    //            }
    //            else if (AttemptsUsed >= _maxAttempts)
    //            {
    //                isGameWon = false;    // Ustawiamy stan przegranej
    //                isGameOver = true;    // Gra zakończona przegraną (wyczerpane próby)
    //            }

    //            return result;  // Zwracamy wynik zgadywania
    //        }

    //        // Metoda ujawniająca kod do odgadnięcia po zakończeniu gry
    //        public string GetAllowedColors() => string.Join(", ", _currentAllowedColors);

    //        // Metoda zwracająca dozwolone kolory jako tablicę znaków
    //        public char[] GetAllowedColorsArray() => _currentAllowedColors;

    //        // Metoda ujawniająca kod do odgadnięcia po zakończeniu gry
    //        public string RevealCode() => isGameOver ? new string(_secretCode) : new string('?', CodeLength);
    //    }
    //}

    //=-=-=-=-=-=--=-=-=-= ZADANIE 5 =-=-=-=-=-=--=-=-=-= 

    public class Game
    {
        private static readonly char[] _colorPool = { 'r', 'y', 'g', 'b', 'm', 'c' }; // Dostępne kolory

        private static readonly char[] _digitPool = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }; // Dostępne cyfry

        private readonly char[] _currentAllowedColors;          // Dozwolone kolory w bieżącej grze
        public int CodeLength { get; private set; }             // Długość kodu do odgadnięcia
        private readonly int _maxAttempts;                      // Maksymalna liczba prób


        private char[] _secretCode;                             // kod do odgadnięcia (w znaczeniu pozycji do odgadnięcia)
        public int AttemptsUsed { get; private set; }           // liczba wykorzystanych prób
        public bool isGameOver { get; private set; }            // czy gra się zakończyła
        public bool isGameWon { get; private set; }             // czy gra została wygrana
        public List<(string Guess, GuessResult Result)> History { get; private set; } // historia zgadywań i ich wyników

        public List<(string Guess, GuessResult Result)> GetHistory() => History; // Metoda zwracająca historię zgadywań

        // Definiujemy konstruktor klasy Game z parametrami n, k, maxAttempts oraz useDigits
        public Game(int n = 6, int k = 4, int maxAttemps = 12, bool useDigits = false)
        {
            // Walidujemy i ustawiamy dozwolone kolory lub cyfry na podstawie wartości n i useDigits
            if (!useDigits)
            {
                // Validacja parametrów n i k dla kolorów
                if (n < 6 || n > 8) throw new ArgumentException("Liczba kolorów (n) musi wynosić 6,7 lub 8.");

                // Validacja długości kodu k dla kolorów
                if (k >= n) throw new ArgumentException("Długość kodu (k) musi być w zakresie 6-8. ");
                _currentAllowedColors = _colorPool.Take(n).ToArray();

            }

            // Ustawiamy długość kodu do odgadnięcia i maksymalną liczbę prób
            else
            {
                // Validacja parametrów n i k dla cyfr
                if (n > 10) throw new ArgumentException("Liczba cyfr (n) musi wynosić max 10.");

                // Validacja długości kodu k dla cyfr
                _currentAllowedColors = _colorPool.Take(n).ToArray();
            }

            // Validacja długości kodu k
            if (k < 3 || k > 6) throw new ArgumentException("Długość kodu (k) musi wynosić 3, 4, 5 lub 6.");
            CodeLength = k;                 // Ustawiamy długość kodu do odgadnięcia
            _maxAttempts = maxAttemps;      // Ustawiamy maksymalną liczbę prób

            // Inicjalizujemy historię zgadywań
            History = new List<(string, GuessResult)>();
            StartNewGame();                 // Rozpoczynamy nową grę
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

        // Metoda do generowania losowego kodu do odgadnięcia
        private char[] GenerateSecretCode()
        {
            // Generujemy losowy kod z dozwolonych kolorów
            Random rnd = new Random();

            // Tworzymy tablicę znaków reprezentującą kod do odgadnięcia
            char[] code = new char[CodeLength];

            // Losujemy kolory dla każdej pozycji w kodzie
            for (int i = 0; i < CodeLength; i++)
            {
                // Wybieramy losowy kolor z dozwolonych kolorów
                code[i] = _currentAllowedColors[rnd.Next(_currentAllowedColors.Length)];
            }

            // Zwracamy wygenerowany kod
            return code;
        }

        // Statyczna metoda do obliczania wyniku zgadywania (trafienia dokładne i niedokładne) bez potrzeby tworzenia instancji gry
        public static GuessResult CalculateScore(string secretCodeStr, string guessStr)
        {
            char[] secret = secretCodeStr.ToCharArray();        // konwertujemy kod sekretny na tablicę znaków
            char[] guess = guessStr.ToCharArray();              // konwertujemy zgadywanie na tablicę znaków
            int len = secret.Length;                            // długość kodu (zakładamy, że oba mają tę samą długość)

            int exactMatches = 0;                               // Inicjalizujemy licznik trafień dokładnych
            bool[] secretMatched = new bool[len];               // tablica do śledzenia trafionych pozycji w kodzie sekretnym
            bool[] guessMatched = new bool[len];                // tablica do śledzenia trafionych pozycji w zgadywaniu

            
        }
    }

}
