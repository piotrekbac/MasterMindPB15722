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
        }
    }
}