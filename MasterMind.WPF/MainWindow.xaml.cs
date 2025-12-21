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


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}