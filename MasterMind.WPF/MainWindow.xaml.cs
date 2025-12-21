using MasterMind.Engine;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// Piotr Bacior - MasterMind Game - nr indeksu: 15 722 - WSEI Kraków

namespace MasterMind.WPF
{

    // Klasa reprezentująca pojedynczy element historii zgadywań w grze MasterMind
    public class HistoryItem
    {
        public int AttemptNumber { get; set; }
        public string GuessCode { get; set; }
        public int Exact { get; set; }
        public int Partial { get; set; }
        public string ResultText { get; set; }
    }

    // Interakcja logiki dla MainWindow.xaml
    public partial class MainWindow : Window
    {
        // Definiujemy pole prywatne do przechowywania instancji gry MasterMind
        private Game _game;

        // Definiujemy dostępne kolory w grze MasterMind w formie tablicy znaków
        private readonly char[] _colors = { 'r', 'y', 'g', 'b', 'm', 'c' }; // dostępne kolory: red, yellow, green, blue, magenta, cyan


        // Definiujemy konstruktor klasy MainWindow
        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        // Metoda do rozpoczęcia nowej gry MasterMind
        private void StartGame()
        {
            _game = new Game();                     // tworzymy nową instancję gry MasterMind
            HistoryList.Items.Clear();              // czyścimy listę historii zgadywań
            StatusText.Text = "Nowa gra rozpoczęta! Odgadnij kod składający się z 4 kolorów.";
            StatusText.Foreground = Brushes.Black;  // ustawiamy kolor tekstu statusu na czarny
            BtnCheck.IsEnabled = true;              // włączamy przycisk sprawdzania zgadywania

            // Inicjalizujemy ComboBoxy dla zgadywania użytkownika - wypełniamy je dostępnymi kolorami
            SetupComboBox(Color1);
            SetupComboBox(Color2);
            SetupComboBox(Color3);
            SetupComboBox(Color4);
        }

        // Obsługa kliknięcia przycisku "Sprawdź" do oceny zgadywania użytkownika
        private void SetupComboBox(ComboBox box)
        {
            // Inicjalizujemy ComboBox dostępnymi kolorami
            box.Items.Clear();

            // Dodajemy nazwy kolorów do ComboBox na podstawie tablicy znaków _colors
            foreach (var c in _colors)
            {
                box.Items.Add(GetColorName(c));     // dodajemy nazwę koloru do ComboBox
            }

            // Ustawiamy domyślnie wybrany indeks na 0 (pierwszy kolor)
            box.SelectedIndex = 0;
        }

        // Metoda obsługująca kliknięcie przycisku sprawdzania zgadywania
        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            // Pobieramy wybrane kolory z ComboBoxów i tworzymy zgadywany kod
            char c1 = GetColorChar(Color1.SelectedItem.ToString());
            char c2 = GetColorChar(Color2.SelectedItem.ToString());
            char c3 = GetColorChar(Color3.SelectedItem.ToString());
            char c4 = GetColorChar(Color4.SelectedItem.ToString());

            string guess = $"{c1}{c2}{c3}{c4}"; // tworzymy zgadywany kod na podstawie wybranych kolorów

            try
            {
                var result = _game.EvaluateGuess(guess);         // oceniamy zgadywanie użytkownika


                // Dodajemy wpis do historii zgadywań
                HistoryList.Items.Add(new HistoryItem
                {
                    AttemptNumber = _game.AttemptsUsed,
                    GuessCode = guess,
                    Exact = result.ExactMatches,
                    Partial = result.PartialMatches,
                    ResultText = result.isVictory ? "Wygrana!" : "Pudło!"
                });                          

                if (result.isVictory)
                {
                    //Użytkownik odgadł kod - aktualizujemy status i wyłączamy przycisk sprawdzania zgadywania
                    StatusText.Text = $"Gratulacje! Odgadłeś kod w {_game.AttemptsUsed} próbach.";
                    StatusText.Foreground = Brushes.Green;            // ustawiamy kolor tekstu statusu na zielony
                    BtnCheck.IsEnabled = false;                       // wyłączamy przycisk sprawdzania zgadywania

                    // Wyświetlamy okno dialogowe z gratulacjami
                    MessageBox.Show("Gratulacje! Odgadłeś kod!", "Wygrana", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Sprawdzamy czy gra się zakończyła bez odgadnięcia kodu
                else if (_game.isGameOver)
                {
                    StatusText.Text = "Koniec gry! Nie udało się odgadnąć kodu.";
                    StatusText.Foreground = Brushes.Red;              // ustawiamy kolor tekstu statusu na czerwony
                    BtnCheck.IsEnabled = false;                       // wyłączamy przycisk sprawdzania zgadywania

                    // Wyświetlamy okno dialogowe informujące o końcu gry
                    MessageBox.Show("Koniec gry! Nie udało się odgadnąć kodu.", "Koniec gry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            // Obsługa wyjątków podczas oceny zgadywania
            catch (System.Exception ex)
            {
                // Obsługa błędów podczas oceny zgadywania - wyświetlamy komunikat o błędzie
                MessageBox.Show($"Błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metoda obsługująca kliknięcie przycisku restartu gry
        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            StartGame(); // rozpoczynamy nową grę po kliknięciu przycisku restartu
        }

        // Definiujemy metodę do mapowania nazwy koloru na odpowiadający znak reprezentujący kolor
        private char GetColorChar(string colorName)
        {
            switch (colorName)
            {
                case "Red": return 'r';
                case "Yellow": return 'y';
                case "Green": return 'g';
                case "Blue": return 'b';
                case "Magenta": return 'm';
                case "Cyan": return 'c';
                default: return 'r'; // zwracamy spację dla nieznanego koloru
            }
        }


        // Metoda obsługująca kliknięcie przycisku sprawdzania zgadywania
        private string GetColorName(char c)
        {
            // Zwracamy nazwę koloru na podstawie znaku reprezentującego kolor
            switch (c)
            {
                case 'r': return "Red";
                case 'y': return "Yellow";
                case 'g': return "Green";
                case 'b': return "Blue";
                case 'm': return "Magenta";
                case 'c': return "Cyan";
                default: return "Unknown";
            }
        }
    }
}